using System;
using System.Collections.Generic;

namespace Builder
{
    class Program
    {
        public class Documento
        {
            public string Intestazione { get; set; }
            public List<RigaDocumento> Righe { get; }
            public double TotaleDocumento { get; set; }
            public Documento()
            {
                Righe = new List<RigaDocumento>();
            }
        }
        public class RigaDocumento
        {
            private string riga;
            private double totale;
            public RigaDocumento(string riga, double totale)
            {
                this.Descrizione = riga;
                this.TotaleRiga = totale;
            }
            public string Descrizione { get; set; }
            public double TotaleRiga { get; set; }
        }

        public void ConstructDocumento(DocumentBuilder builder)
        {
            builder.AggiungiIntestazione(intestazione);
            foreach (var riga in Righe)
            {
                builder.AggiungiRiga(riga.desc, riga.importo);
            }
            builder.AggiungiTotale(totale);
        }
        
        public abstract class DocumentBuilder
        {
            protected Documento doc;
            public abstract void AggiungiIntestazione(string str);
            public abstract void AggiungiRiga(string riga, double totale);
            public abstract void AggiungiTotale(double totale);
        }

        public class JsonDocBuilder : DocumentBuilder
        {
            public JsonDocBuilder()
            {
                doc = new Documento();
            }
            public override void AggiungiIntestazione(string str)
            {
                doc.Intestazione = str;
            }

            public override void AggiungiRiga(string riga, double totale)
            {
                doc.Righe.Add(new RigaDocumento(riga, totale));
            }

            public override void AggiungiTotale(double totale)
            {
                doc.TotaleDocumento = totale;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }


}
