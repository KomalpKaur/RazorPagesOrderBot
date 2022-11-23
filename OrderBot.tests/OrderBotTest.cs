using System;
using System.IO;
using Xunit;
using OrderBot;

namespace OrderBot.tests
{
    public class OrderBotTest
    {
        // [Fact]
        // public void Test1()
        // {

        // }
        [Fact]
        public void TestWelcome()
        {
            Session oSession = new Session("12345");
            String sInput = oSession.OnMessage("hello")[0];
            Assert.True(sInput.Contains("Welcome"));
        }
        [Fact]
        public void TestAirlineTicket()
        {
            Session oSession = new Session("12345");
            String sInput = oSession.OnMessage("hello")[0];
            Assert.True(sInput.Contains("Airline"));
            Assert.True(sInput.Contains("Ticket"));

        }
        [Fact]
        public void TestTravelDate()
        {
            Session oSession = new Session("12345");
            String sInput = oSession.OnMessage("hello")[2];
            Assert.True(sInput.ToLower().Contains("dates"));
            Assert.True(sInput.ToLower().Contains("book"));
        }
        [Fact]
        public void TestTravelTime()
        {
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            String sInput = oSession.OnMessage("november 20")[0];
            Assert.True(sInput.ToLower().Contains("time"));
            Assert.True(sInput.ToLower().Contains("travel"));
        }
        [Fact]
        public void TestDestination()
        {
            string sPath = DB.GetConnectionString();
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("november 20");
            String sInput = oSession.OnMessage("4 pm")[0];
            Assert.True(sInput.ToLower().Contains("destination"));
            Assert.True(sInput.ToLower().Contains("name"));
        }
        [Fact]
        public void TestClass()
        {
            string sPath = DB.GetConnectionString();
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("november 20");
            oSession.OnMessage("4 pm");
            String sInput = oSession.OnMessage("calgary")[0];
            Assert.True(sInput.ToLower().Contains("class"));
            Assert.True(sInput.ToLower().Contains("travel"));
            Assert.True(sInput.Contains("Business"));
            Assert.True(sInput.Contains("Economy"));
        }
        [Fact]
        public void TestThankyou()
        {
            string sPath = DB.GetConnectionString();
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("november 20");
            oSession.OnMessage("4 pm");
            oSession.OnMessage("calgary");
            String sInput = oSession.OnMessage("economy")[0];
            Assert.True(sInput.ToLower().Contains("booking"));
            Assert.True(sInput.ToLower().Contains("trip"));
            Assert.True(sInput.Contains("Thankyou"));
        }
    }
}
