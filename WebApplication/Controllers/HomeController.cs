using DBLibrary.Entity;
using DBLibrary.Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private SCMEntity sCMEntity;

        public HomeController(SCMEntity sCMEntity)
        {
            this.sCMEntity = sCMEntity;
        }

        public ActionResult Index()
        {

            LoadViewBags();

            return View();
        }

        public ActionResult SKUItems()
        {
            var model = ProductService.GetItems();
            return View(model);
        }

        [HttpPost]
        public ActionResult SKUItems(Item model)
        {
            return View();
        }

        public ActionResult Promotions()
        {
            var model = ProductService.GetPromotionItems();
            return View(model);
        }

        [HttpPost]
        public ActionResult Promotions(PromoItem model)
        {
            return View();
        }

        public void LoadViewBags()
        {
            ViewData["ItemsList"] = new SelectList(ProductService.GetItems(), "ItemId", "ItemName");
        }

        [HttpPost]
        public JsonResult GetItemPrice(Guid ItemId, int Qty)
        {
            var sessionlist = new List<SessionVM>();
            if (Session["Itemslist"] != null)
            {
                sessionlist = (List<SessionVM>)Session["Itemslist"];
            }
            var CalcPrice = ProductService.GetItemCalcPrice(ItemId, Qty, sessionlist);
            return Json(new { data = CalcPrice });
        }

        [HttpPost]
        public JsonResult SetItemsSession(Guid ItemId, string ItemName, int Qty, decimal Amount)
        {
            if (Session["Itemslist"] != null)
            {
                var sessionlist = (List<SessionVM>)Session["Itemslist"];
                SessionVM itemObj = new SessionVM();
                itemObj.ItemId = ItemId;
                itemObj.Qty = Qty;
                itemObj.ItemName = ItemName;
                itemObj.Amount = Amount;
                sessionlist.Add(itemObj);
                Session["Itemslist"] = sessionlist;
            }
            else
            {
                var itemlst = new List<SessionVM>();
                SessionVM itemObj = new SessionVM();
                itemObj.ItemId = ItemId;
                itemObj.Qty = Qty;
                itemObj.ItemName = ItemName;
                itemObj.Amount = Amount;
                itemlst.Add(itemObj);
                Session["Itemslist"] = itemlst;
            }

            return Json(new { data = "" });
        }

        [HttpPost]
        public JsonResult ClearSession()
        {
            Session["Itemslist"] = null;
            Session.Abandon();
            Session.Clear();
            return Json(new { data = "" });
        }
    }
}