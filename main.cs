using System;
using System.Net;

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("HttpListener running on http://*:8002/");

        string[] s3Facts =
        {
            "Scale storage resources to meet fluctuating needs with 99.999999999% (11 9s) of data durability.",
            "Store data across Amazon S3 storage classes to reduce costs without upfront investment or hardware refresh cycles.",
            "Protect your data with unmatched security, compliance, and audit capabilities.",
            "Easily manage data at any scale with robust access controls, flexible replication tools, and organization-wide visibility.",
            "Run big data analytics, artificial intelligence (AI), machine learning (ML), and high performance computing (HPC) applications.",
            "Meet Recovery Time Objectives (RTO), Recovery Point Objectives (RPO), and compliance requirements with S3's robust replication features."
        };

        var listener = new HttpListener();
        listener.Prefixes.Add("http://*:8002/");
        listener.Start();

        try
        {
            var rnd = new Random();
            while (true)
            {
                var ctx = listener.GetContext();
                using var response = ctx.Response;

                response.StatusCode = (int)HttpStatusCode.OK;
                response.StatusDescription = "OK";

                int i = rnd.Next(s3Facts.Length);
                string message = $"{DateTime.Now:T} - {s3Facts[i]}";

                Console.WriteLine(message);

                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(message);
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        finally
        {
            listener.Close();
        }
    }
}

