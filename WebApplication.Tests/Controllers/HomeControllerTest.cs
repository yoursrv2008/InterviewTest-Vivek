using DBLibrary.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WebApplication.Models;

namespace WebApplication.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        public Guid ItemIdA = Guid.Parse("580BC183-16DF-42A4-A6E6-36691C424C6F");
        public Guid ItemIdB = Guid.Parse("E2F0B74E-FC18-462D-B11C-712E22302AE1");
        public Guid ItemIdC = Guid.Parse("AF29DF4E-4D07-4EB7-856B-324919671529");
        public Guid ItemIdD = Guid.Parse("1F999882-053C-40F3-AB7E-B208D28CCE50");

        [TestMethod]
        public void Test_ScenarioA()
        {
            var scenarioAitems = GetTestScenarioAItems();
            var sessionlist = new List<SessionVM>();
            var result = new decimal(0);
            foreach (var item in scenarioAitems)
            {
                SessionVM obj = new SessionVM();
                obj.ItemId = item.ItemId;
                obj.Qty = item.Qty;
                obj.Amount = ProductService.GetItemCalcPrice(item.ItemId, item.Qty, sessionlist).Item2;
                result += obj.Amount;
                sessionlist.Add(obj);
            }


            Assert.IsNotNull(result);
            Assert.AreEqual(result, 100, "Scenario A Ok..");
        }

        List<SessionVM> GetTestScenarioAItems()
        {
            List<SessionVM> lst = new List<SessionVM>();
            lst.Add(new SessionVM() { ItemId = ItemIdA, ItemName = "A", Qty = 1, });
            lst.Add(new SessionVM() { ItemId = ItemIdB, ItemName = "B", Qty = 1, });
            lst.Add(new SessionVM() { ItemId = ItemIdC, ItemName = "C", Qty = 1, });
            return lst;
        }

        [TestMethod]
        public void Test_ScenarioB()
        {
            var scenarioBitems = GetTestScenarioBItems();
            var sessionlist = new List<SessionVM>();
            var result = new decimal(0);
            foreach (var item in scenarioBitems)
            {
                SessionVM obj = new SessionVM();
                obj.ItemId = item.ItemId;
                obj.Qty = item.Qty;
                obj.Amount = ProductService.GetItemCalcPrice(item.ItemId, item.Qty, sessionlist).Item2;
                result += obj.Amount;
                sessionlist.Add(obj);
            }


            Assert.IsNotNull(result);
            Assert.AreEqual(result, 100, "Scenario B Ok..");
        }

        List<SessionVM> GetTestScenarioBItems()
        {
            List<SessionVM> lst = new List<SessionVM>();
            lst.Add(new SessionVM() { ItemId = ItemIdA, ItemName = "A", Qty = 5, });
            lst.Add(new SessionVM() { ItemId = ItemIdB, ItemName = "B", Qty = 5, });
            lst.Add(new SessionVM() { ItemId = ItemIdC, ItemName = "C", Qty = 1, });
            return lst;
        }

        [TestMethod]
        public void Test_ScenarioC()
        {
            var scenarioCitems = GetTestScenarioCItems();
            var sessionlist = new List<SessionVM>();
            var result = new decimal(0);
            foreach (var item in scenarioCitems)
            {
                SessionVM obj = new SessionVM();
                obj.ItemId = item.ItemId;
                obj.Qty = item.Qty;
                obj.Amount = ProductService.GetItemCalcPrice(item.ItemId, item.Qty, sessionlist).Item2;
                result += obj.Amount;
                sessionlist.Add(obj);
            }


            Assert.IsNotNull(result);
            Assert.AreEqual(result, 100, "Scenario C Ok..");
        }

        List<SessionVM> GetTestScenarioCItems()
        {
            List<SessionVM> lst = new List<SessionVM>();
            lst.Add(new SessionVM() { ItemId = ItemIdA, ItemName = "A", Qty = 3, });
            lst.Add(new SessionVM() { ItemId = ItemIdB, ItemName = "B", Qty = 5, });
            lst.Add(new SessionVM() { ItemId = ItemIdC, ItemName = "C", Qty = 1, });
            lst.Add(new SessionVM() { ItemId = ItemIdD, ItemName = "D", Qty = 1, });
            return lst;
        }
    }
}
