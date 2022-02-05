using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    private readonly static byte[] Key = Encoding.ASCII.GetBytes("+KbPeShVmYq3t6w9z$C&E)H@McQfTjWn");
    private readonly static byte[] IV = Encoding.ASCII.GetBytes("UjXn2r5u8x/A?D(G");

    public static void SaveBin(object obj, string path)
    {
        var formatter = new BinaryFormatter();
        using (var sW = new FileStream(path, FileMode.OpenOrCreate))
            formatter.Serialize(sW, obj);
    }

    public static T LoadBin<T>(string path)
    {
        if (File.Exists(path))
        {
            var formatter = new BinaryFormatter();
            using (var sR = new FileStream(path, FileMode.Open))
            {
                T savedData = (T)formatter.Deserialize(sR);
                return savedData;
            }
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return default;
        }
    }

    public static void SaveEncrypted(object obj, string path)
    {
        using (Aes newAes = Aes.Create())
        {
            newAes.Mode = CipherMode.CBC;
            newAes.Key = Key;
            newAes.IV = IV;

            var encryptor = newAes.CreateEncryptor(Key, IV);

            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                        swEncrypt.Write(Convert.ToBase64String(ObjectToByteArray(obj)));

                    SaveBin(msEncrypt.ToArray(), path);
                }
            }
        }
    }

    public static T LoadEncrypted<T>(string path)
    {
        using (var aes = new AesManaged())
        {
            var decryptor = aes.CreateDecryptor(Key, IV);
            aes.Mode = CipherMode.CBC;

            using (var ms = new MemoryStream(LoadBin<byte[]>(path)))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    using (var reader = new StreamReader(cs))
                        return (T)ByteArrayToObject(Convert.FromBase64String(reader.ReadToEnd()));
        }
    }

    private static byte[] ObjectToByteArray(object obj)
    {
        if (obj == null)
            return null;

        var bf = new BinaryFormatter();
        var ms = new MemoryStream();
        bf.Serialize(ms, obj);

        return ms.ToArray();
    }

    private static object ByteArrayToObject(byte[] arrBytes)
    {
        var memStream = new MemoryStream();
        var binForm = new BinaryFormatter();
        memStream.Write(arrBytes, 0, arrBytes.Length);
        memStream.Seek(0, SeekOrigin.Begin);
        var obj = binForm.Deserialize(memStream);

        return obj;
    }
}
