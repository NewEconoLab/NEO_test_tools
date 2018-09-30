using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace NEO_test_tools.lib
{
    public class neoHelper
    {
        public string Sign(string script, string prikeyHex)
        {
            return ThinNeo.Helper.Sign(script.HexString2Bytes(), prikeyHex.HexString2Bytes()).ToHexString();
        }

        public JObject sendrawtransaction(string neoCliJsonRPCUrl, string txSigned)
        {
            httpHelper hh = new httpHelper();
            var resp = hh.Post(neoCliJsonRPCUrl, "{'jsonrpc':'2.0','method':'sendrawtransaction','params':['" + txSigned + "'],'id':1}", System.Text.Encoding.UTF8, 1);

            bool isSendSuccess = (bool)JObject.Parse(resp)["result"];
            JObject Jresult = new JObject();
            Jresult.Add("sendrawtransactionresult", isSendSuccess);
            if (isSendSuccess)
            {
                ThinNeo.Transaction lastTran = new ThinNeo.Transaction();
                lastTran.Deserialize(new MemoryStream(txSigned.HexString2Bytes()));
                string txid = lastTran.GetHash().ToString();

                ////从已签名交易体分析出未签名交易体，并做Hash获得txid
                //byte[] txUnsigned = txSigned.Split("014140")[0].HexString2Bytes();
                //string txid = ThinNeo.Helper.Sha256(ThinNeo.Helper.Sha256(txUnsigned)).Reverse().ToArray().ToHexString();

                Jresult.Add("txid", txid);
            }
            else
            {
                //上链失败则返回空txid
                Jresult.Add("txid", string.Empty);
            }

            return Jresult;
        }

        public JObject sendTxPlusSign(string neoCliJsonRPCUrl, string txScriptHex, string signHex, string publicKeyHex)
        {
            byte[] txScript = txScriptHex.HexString2Bytes();
            byte[] sign = signHex.HexString2Bytes();
            byte[] pubkey = publicKeyHex.HexString2Bytes();
            //byte[] prikey = privateKeyHex.HexToBytes();

            //byte[] sign = null;

            //sign = ThinNeo.Helper.Sign(txScript, prikey);

            //var pubkey = ThinNeo.Helper.GetPublicKeyFromPrivateKey(prikey);

            var addr = ThinNeo.Helper.GetAddressFromPublicKey(pubkey);

            ThinNeo.Transaction lastTran = new ThinNeo.Transaction();
            lastTran.Deserialize(new MemoryStream(txScript));
            lastTran.witnesses = null;
            lastTran.AddWitness(sign, pubkey, addr);

            string TxPlusSignStr = string.Empty;
            using (var ms = new System.IO.MemoryStream())
            {
                lastTran.Serialize(ms);
                TxPlusSignStr = ms.ToArray().ToHexString();
            }

            return sendrawtransaction(neoCliJsonRPCUrl, TxPlusSignStr);
        }

        public string getTransferTxHex(JArray utxoJA, string addrOut, string addrIn, string assetID, decimal amounts, int outputNum = 1)
        {
            ////string findFliter = "{addr:'" + addrOut + "',used:''}";
            ////JArray outputJA = mh.GetData(mongodbConnStr, mongodbDatabase, "utxo", findFliter);

            ////linq查找指定asset
            //var query = from utxos in utxoJA.Children()
            //            where (string)utxos["asset"] == assetID
            //            orderby (decimal)utxos["value"] //descending
            //            select utxos;
            ////var utxo = query.ToList()[0];

            //JArray utxo2pay = new JArray();
            //decimal utxo_value = 0; //所有utxo总值
            //foreach (JObject utxo in query)
            //{
            //    if (utxo_value < amounts)//如utxo总值小于需支付则继续加utxo
            //    {
            //        utxo2pay.Add(utxo);
            //        utxo_value += (decimal)utxo["value"];
            //    }
            //    else { break; }//utxo总值大于等于需支付则跳出
            //}
            ////byte[] utxo_txid = ThinNeo.Debug.DebugTool.HexString2Bytes(((string)utxo["txid"]).Replace("0x", "")).Reverse().ToArray();
            ////ushort utxo_n = (ushort)utxo["n"];
            ////decimal utxo_value = (decimal)utxo["value"];           

            //if (amounts > utxo_value)
            //{
            //    return string.Empty;
            //}

            JArray utxo2pay = utxoJA;
            if (utxo2pay == new JArray()) { return string.Empty; }

            byte[] assetBytes = assetID.Replace("0x", "").HexString2Bytes().Reverse().ToArray();

            ThinNeo.Transaction lastTran;
            lastTran = new ThinNeo.Transaction
            {
                type = ThinNeo.TransactionType.ContractTransaction,//转账
                attributes = new ThinNeo.Attribute[0],
                inputs = new ThinNeo.TransactionInput[utxo2pay.Count]
            };
            //构造输入
            decimal utxo_value = 0;//所有utxo总金额
            int i = 0;
            foreach (var utxo in utxo2pay)
            {
                lastTran.inputs[i] = new ThinNeo.TransactionInput
                {
                    hash = ((string)utxo["txid"]).Replace("0x", "").HexString2Bytes().Reverse().ToArray(),
                    index = (ushort)utxo["n"]
                };
                utxo_value += (decimal)utxo["value"];//加总所有utxo金额
                i++;
            }

            decimal amountOfEachOutput = ((decimal)(int)((amounts / outputNum) * 100000000)) / 100000000;
            bool isNeedRefund = (utxo_value - (amountOfEachOutput * outputNum)) > 0 ? true : false;

            if (isNeedRefund)
            {
                lastTran.outputs = new ThinNeo.TransactionOutput[outputNum + 1];
            }
            else
            {
                lastTran.outputs = new ThinNeo.TransactionOutput[outputNum];
            }

            
            for (int ii = 0; ii < outputNum; ii++)
            {
                lastTran.outputs[ii] = new ThinNeo.TransactionOutput
                {
                    assetId = assetBytes,
                    toAddress = ThinNeo.Helper.GetPublicKeyHashFromAddress(addrIn),
                    value = amountOfEachOutput
                };//给对方转账
            }

            if (isNeedRefund)
            {
                lastTran.outputs[outputNum] = new ThinNeo.TransactionOutput
                {
                    assetId = assetBytes,
                    toAddress = ThinNeo.Helper.GetPublicKeyHashFromAddress(addrOut),
                    value = utxo_value - (amountOfEachOutput * outputNum)
                };
            }//如需要，处理给自己找零

            using (var ms = new MemoryStream())
            {
                lastTran.SerializeUnsigned(ms);
                return ms.ToArray().ToHexString();
            }
        }
    }
}
