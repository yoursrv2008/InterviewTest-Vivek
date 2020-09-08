using DBLibrary.Entity;
using DBLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Models;

namespace DBLibrary.Services
{
    public static class ProductService
    {
        public static List<Item> GetItems()
        {
            List<Item> lstitems = new List<Item>();
            try
            {
                using (var context = new SCMEntity())
                {
                    var rtnmodel = context.Items.OrderBy(x => x.ItemName).ToList();

                    return rtnmodel;
                }
            }
            catch (Exception exe)
            {
                return lstitems;
            }
        }

        public static List<PromoItemsVM> GetPromotionItems()
        {
            List<PromoItemsVM> lstitems = new List<PromoItemsVM>();
            try
            {
                using (var context = new SCMEntity())
                {
                    var rtnmodel = (from s in context.PromoItems
                                    join i in context.Items on s.ItemId equals i.ItemId
                                    orderby i.ItemName
                                    select new PromoItemsVM
                                    {
                                        ItemId = s.ItemId,
                                        PromoItemId = s.PromoItemId,
                                        ItemName = i.ItemName,
                                        PromotionalQty = s.PromotionalQty,
                                        FixedPrice = s.FixedPrice,
                                        GroupId = s.GroupId ?? 0
                                    }).ToList();
                    return rtnmodel;
                }
            }
            catch (Exception ex)
            {
                return lstitems;
            }
        }

        public static Tuple<bool, decimal, List<SessionVM>> GetItemCalcPrice(Guid ItemId, int Qty, List<SessionVM> sessionObj = null)
        {
            var promoObj = GetPromotionItems().FirstOrDefault(x => x.ItemId == ItemId);
            var itemObj = GetItems().FirstOrDefault(x => x.ItemId == ItemId);
            var grouplstObj = GetPromotionItems().Where(x => x.GroupId == promoObj.GroupId && x.GroupId != 0).ToList();

            var rtnPrice = new decimal(0);

            if (Qty < promoObj.PromotionalQty)
            {
                rtnPrice += Qty * itemObj.UnitPrice.Value;
                return new Tuple<bool, decimal, List<SessionVM>>(true, rtnPrice, sessionObj);
            }
            while (Qty != 0)
            {
                if (grouplstObj.Count() > 0)
                {
                    var groupOtherItems = grouplstObj.Where(x => x.ItemId != ItemId).ToList(); //.Select(x => x.ItemId).ToArray();
                    if (groupOtherItems.Count() > 0)
                    {
                        if(sessionObj != null)
                        {
                            foreach(var item in sessionObj)
                            {                                
                                if (grouplstObj.Where(x => x.ItemId == item.ItemId).ToList().Count()>0)
                                {
                                    var removeItem = groupOtherItems.FirstOrDefault(x => x.ItemId == item.ItemId);
                                    item.Amount = 0;
                                    groupOtherItems.Remove(removeItem);
                                }                                
                            }                            
                        }
                        if(groupOtherItems.Count() > 0)
                        {
                            rtnPrice = itemObj.UnitPrice.Value;
                            Qty = Qty - promoObj.PromotionalQty;
                        }
                        else
                        {
                            rtnPrice = promoObj.FixedPrice;
                            Qty = Qty - promoObj.PromotionalQty;
                        }
                       
                    }
                }
                else if (Qty >= promoObj.PromotionalQty)
                {
                    rtnPrice += promoObj.FixedPrice;
                    Qty = Qty - promoObj.PromotionalQty;
                }
                else
                {
                    rtnPrice += Qty * itemObj.UnitPrice.Value;
                    Qty = 0;
                }
            }
            if (rtnPrice > 0)
            {
                return new Tuple<bool, decimal, List<SessionVM>>(true, rtnPrice, sessionObj);
            }
            return new Tuple<bool, decimal, List<SessionVM>>(false, rtnPrice, sessionObj);
        }
         
    }
}
