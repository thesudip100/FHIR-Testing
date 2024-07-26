using System;
using System.Collections.Generic;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;

namespace FHIR
{
    public static class Program
    {
        private const string _fhirServer = "https://server.fire.ly";
        static void Main(string[] args)
        {
            FhirClient fhirClient = new FhirClient(_fhirServer);
            Bundle patientBundle = fhirClient.Search<Patient>(new string[] { "name=test" });
            

            int patientNumber = 0;

            while(patientBundle!=null)
            {

                Console.WriteLine($"Total:{patientBundle.Total} Entry Count:{patientBundle.Entry.Count}");

                //list eaach patient in the bundle
                foreach (Bundle.EntryComponent entry in patientBundle.Entry)
                {
                    Console.WriteLine($"-Entry {patientNumber,3}:{entry.FullUrl}");
                    if (entry.Resource != null)
                    {
                        Patient patient = (Patient)entry.Resource;
                        Console.WriteLine($" - ID: {patient.Id}");

                        if (patient.Name.Count > 0)
                        {
                            Console.WriteLine($"- Name: {patient.Name[0].ToString()} ");
                        }

                        patientNumber++;
                    }
                }
                //get more results
                patientBundle =fhirClient.Continue(patientBundle);
            }
           
        }
    }
}