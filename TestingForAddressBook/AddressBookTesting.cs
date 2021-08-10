using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace TestingForAddressBook
{
    [TestClass]
    public class AddressBookTesting
    {
        //seting the restclient as null
        RestClient client = null;
        [TestInitialize]
        public void SetUp()
        {
            client = new RestClient("http://localhost:3000");
        }
        public IRestResponse GetAllContacts()
        {
            //Define method Type
            RestRequest request = new RestRequest("/AddressBookContacts", Method.GET);
            //Eexcute request
            IRestResponse response = client.Execute(request);
            //Return the response
            return response;
        }
        //UC 1: Getting all the employee details from json server
        [TestMethod]
        public void OnCallingGetMethodWeAreReturningContactData()
        {
            IRestResponse response = GetAllContacts();
            //Deserialize json object to List
            var jsonObject = JsonConvert.DeserializeObject<List<AddressBookData>>(response.Content);
            foreach(var i in jsonObject)
            {
                Console.WriteLine($"Id : {i.id}| Name : {i.firstName+" "+i.lastName}| Address : {i.address+" "+i.city+" "+i.state} | Zip : {i.zip}|" +
                    $"PhoneNumber:{i.phoneNumber}|Email:{i.Email}");
            }
            //Check by count 
            Assert.AreEqual(3, jsonObject.Count);
            //Check by status code 
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        //UC 2:Add Multiple Contact to json server
        public IRestResponse AddingInJsonServer(JsonObject jsonObject)
        {
            RestRequest request = new RestRequest("/AddressBookContacts", Method.POST);
            request.AddParameter("application/json", jsonObject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return response;
        }
        [TestMethod]
        public void UseingPostMethodToAddMultipleContacts()
        {
            //Create Json object for employee1
            JsonObject employee1 = new JsonObject();
            employee1.Add("firstName", "Ravi");
            employee1.Add("lastName", "Kiran");
            employee1.Add("address", "Gandi nagar");
            employee1.Add("city", "Kakinada");
            employee1.Add("state", "TN");
            employee1.Add("zip", 8876);
            employee1.Add("phoneNumber", 994848849);
            employee1.Add("Email", "Ravi@gmail.com");
            //Call Function to Add and change that result to status code
            HttpStatusCode response1 = AddingInJsonServer(employee1).StatusCode;

            //Create Json object for employee2
            JsonObject employee2 = new JsonObject();
            employee2.Add("firstName", "Ram");
            employee2.Add("lastName", "K");
            employee2.Add("address", "Gandi street");
            employee2.Add("city", "Kerala");
            employee2.Add("state", "TN");
            employee2.Add("zip", 8876);
            employee2.Add("phoneNumber", 884848849);
            employee2.Add("Email", "Ram@gmail.com");
            //Call Function to Add and change that result to status code
            HttpStatusCode response2 = AddingInJsonServer(employee2).StatusCode;

            Assert.AreEqual(response1, HttpStatusCode.Created);
            Assert.AreEqual(response2, HttpStatusCode.Created);
        }
        //UC 3:Update Values in json server useing id
        [TestMethod]
        public void UseingPUTMethodToUpdateContactData()
        {
            RestRequest request = new RestRequest("/AddressBookContacts/2", Method.PUT);
            JsonObject json = new JsonObject();
            json.Add("firstName", "vijay");
            json.Add("lastName", "Kiran");
            json.Add("address", "Gandi nagar");
            json.Add("city", "Kakinada");
            json.Add("state", "TN");
            json.Add("zip", 8876);
            json.Add("phoneNumber", 994848849);
            json.Add("Email", "vijay@gmail.com");
            //directly adding json object to request
            request.AddJsonBody(json);
            IRestResponse response = client.Execute(request);
            var result = JsonConvert.DeserializeObject<AddressBookData>(response.Content);
            Console.WriteLine($"Id : {result.id}| Name : {result.firstName + " " + result.lastName}| Address : {result.address + " " + result.city + " " + result.state} | Zip : {result.zip}|" +
                    $"PhoneNumber:{result.phoneNumber}|Email:{result.Email}");
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }
        // Usecase 4: Delete the employee details using the id
        [TestMethod]
        public void UseingDELETEMethodToDeleteContact()
        {
            RestRequest request = new RestRequest("/AddressBookContacts/2", Method.DELETE);
            IRestResponse response = client.Execute(request);
            //checking the data after delete
            IRestResponse getresponse = GetAllContacts();
            var result = JsonConvert.DeserializeObject<List<AddressBookData>>(getresponse.Content);
            foreach (var i in result)
            {
                Console.WriteLine($"Id : {i.id}| Name : {i.firstName + " " + i.lastName}| Address : {i.address + " " + i.city + " " + i.state} | Zip : {i.zip}|" +
                    $"PhoneNumber:{i.phoneNumber}|Email:{i.Email}");
            }
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
