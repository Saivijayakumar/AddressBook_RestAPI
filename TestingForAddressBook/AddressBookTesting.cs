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
        public IRestResponse GetAllEmployees()
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
            IRestResponse response = GetAllEmployees();
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
    }
}
