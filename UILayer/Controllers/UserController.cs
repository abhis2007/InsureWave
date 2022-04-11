using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using RepoLayer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using UILayer.Models;
using Microsoft.AspNetCore.Http;

namespace UILayer.Controllers
{
    public class UserController : Controller
    {
        IUser obj;
        public UserController(IUser _obj)
        {
            obj = _obj;
        }

        public IActionResult CreateUser()
        {
            var RolesList=obj.AllRole();
            var GenList = obj.AllGender(); 
            ViewBag.RoleList = obj.AllRole().ConvertAll(x => {
                return new SelectListItem
                {
                    Text = x.RoleName,
                    Value = x.RoleId.ToString()
                };
            });
            ViewBag.GenderList = new SelectList(GenList, "GenderId", "GenderName");

            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(NewUser _NewUser)
        {
            User _user = new User() { UserId=_NewUser.UserId,FirstName=_NewUser.FirstName,LastName=_NewUser.LastName,GenderId=_NewUser.GenderId,EmailId=_NewUser.EmailId,Password=_NewUser.Password,RoleId=_NewUser.RoleId};
            var RolesList = obj.AllRole();
            var GenList = obj.AllGender();
            ViewBag.RoleList = obj.AllRole().ConvertAll(x=> {
                return new SelectListItem
                {
                    Text=x.RoleName,
                    Value=x.RoleId.ToString()
                };
            });
            ViewBag.GenderList = new SelectList(GenList, "GenderId", "GenderName");
            
            if (obj.GetUserById(_user.UserId) != null)
            {
                ViewBag.IsValidId =_user.UserId +" already exist";
                return View();
            }
            if (obj.GetUserByMail(_user.EmailId) != null)
            {
                ViewBag.IsValidMail = _user.EmailId + " already exist";
                return View();
            }
            obj.AddUser(_user);
            if (_user.RoleId == 1) //Broker
            {
                Broker broker = new Broker() { BrokerId = "BR_" + _user.UserId, UserId = _user.UserId };
                obj.AddBroker(broker);
            }
            if (_user.RoleId == 6)
            {
                Insurer insurer = new Insurer() {InsurerId="IR_"+_user.UserId,UserId=_user.UserId };
                obj.AddInsurer(insurer);
            }
            return RedirectToAction("CreateUser");
        }

        public IActionResult LoginPage()
        {
            var RolesList = obj.AllRole();
            ViewBag.RoleList = new SelectList(RolesList, "RoleId", "RoleName");
            return View();
        }

        [HttpPost]
        public IActionResult LoginPage(Login model)
        {
            if (obj.IsValid(model.UserName, model.Password))
            {
                HttpContext.Session.SetString("UserName",model.UserName);
                User _user= obj.GetUserById(model.UserName);
                if (_user.RoleId == model.RoleId && model.RoleId == 1)return RedirectToAction("Broker");
                if (_user.RoleId == model.RoleId && model.RoleId == 6) return RedirectToAction("Insurer");
                if (_user.RoleId == model.RoleId && model.RoleId == 11)return RedirectToAction("BuyerPageAfterLogin");
            }
            ViewBag.Msg = "Invalid attempt";
            ViewBag.RoleList = new SelectList(obj.AllRole(), "RoleId", "RoleName");
            return View();
        }

        public IActionResult BuyerPageAfterLogin()
        {
            string UserName = HttpContext.Session.GetString("UserName");
            if (UserName == null) return RedirectToAction("LoginPage");
            ViewBag.Assets = obj.GetAssets().ConvertAll(x => {
                return new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.AssetId.ToString()
                };
            });
            ViewBag.Countries = obj.GetCountries().ConvertAll(x => {
                return new SelectListItem
                {
                    Text = x.CountryName,
                    Value = x.CountryId.ToString()
                };
            });
            ViewBag.Requests = obj.AllRequest().ConvertAll(x => {
                return new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                };
            });
            ViewBag.AllBrokers = obj.GetAllUserFromRoleId(1).ConvertAll(x => {
                return new SelectListItem
                {
                    Text = x.FirstName + " " + x.LastName + "(" + x.UserId + ")",
                    Value = x.UserId.ToString()
                };
            });

            ViewBag.AllAssetOfVessel = obj.AllVeselAssetsByuserId(UserName);
            ViewBag.AllAsset = obj.GetAssets();
            ViewBag.AllCountry = obj.GetCountries();
            return View();
        }

        [HttpPost]
        public IActionResult BuyerPageAfterLogin(BuyerAssetVessel _val)
        {
            _val.UserId =HttpContext.Session.GetString("UserName");
            _val.BrokerId = "BR_" + _val.BrokerId;
            ViewBag.Assets = obj.GetAssets().ConvertAll(x=> {
                return new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.AssetId.ToString()
                };
            });
            ViewBag.Countries = obj.GetCountries().ConvertAll(x=>{
                return new SelectListItem {
                    Text=x.CountryName,
                    Value=x.CountryId.ToString()
                };
            });
            ViewBag.Requests = obj.AllRequest().ConvertAll(x=> {
                return new SelectListItem {
                    Text = x.Name,
                    Value = x.Id.ToString()
                };
            });
            ViewBag.AllBrokers = obj.GetAllUserFromRoleId(1).ConvertAll(x => {
                return new SelectListItem
                {
                    Text = x.FirstName+" "+x.LastName+"("+x.UserId+")",
                    Value = x.UserId.ToString()
                };
            });

            ViewBag.AllAssetOfVessel = obj.AllVeselAssetsByuserId(_val.UserId);
            ViewBag.AllAsset = obj.GetAssets();
            ViewBag.AllCountry = obj.GetCountries();

            BuyerAssetVessel ExistingAsset = obj.getAssetOfVesselByUserIdAssetId(_val.UserId,_val.AssetId);
            
            if (ExistingAsset == null)
            {
                obj.AddAssetInBuyerVessel(_val);
                AddInFeedbackFromBuyerPagerAfterLoginIaction(_val,ExistingAsset);
                return RedirectToAction("BuyerPageAfterLogin");
            }
            if (obj.GetAssetFromBrokerBuyerByBuyerIdAssetId(_val.UserId, _val.AssetId) != null)
            {
                //dont update bcoz forwarded by broker to 
                return RedirectToAction("BuyerPageAfterLogin");

            }
            obj.UpdateAssetOfBuyerByAIdUId(_val.AssetId,_val.UserId, _val);
            Feedback f = new Feedback() { UserId = _val.UserId, AssetId = _val.AssetId, BuyerId = _val.UserId, Response = "Asset vessel updated_1" };
            obj.AddInFeedback(f);
            AddInFeedbackFromBuyerPagerAfterLoginIaction(_val,ExistingAsset);
            return RedirectToAction("BuyerPageAfterLogin");
        }

        public void AddInFeedbackFromBuyerPagerAfterLoginIaction(BuyerAssetVessel _val,BuyerAssetVessel ExistingAsset)
        {
            if (_val.RequestStatus ==11)
            {
                Feedback f = new Feedback() { UserId = _val.UserId, AssetId = _val.AssetId, BuyerId = _val.UserId, Response = "Pending at buyer end_0" };
                obj.AddInFeedback(f);
            }
            else
            {
                Feedback f = new Feedback() { UserId = _val.UserId, AssetId = _val.AssetId, BuyerId = _val.UserId, Response = "Sent to broker_1" };
                obj.AddInFeedback(f);

            }
        }

        public IActionResult Broker()
        {
            string UserName=HttpContext.Session.GetString("UserName");
            if (UserName == null) return RedirectToAction("LoginPage");
            ViewBag.AllAssetOfVessel = obj.AllVeselAssetsByBrokerId("BR_"+UserName);
            ViewBag.AllAsset = obj.GetAssets();
            ViewBag.AllBrokerBuyerAsset = obj.AllAssetOfBrokerBuyer();
            ViewBag.AllCountry = obj.GetCountries();
            ViewBag.AllInsurer = obj.GetAllUserFromRoleId(obj.GetRoleByRoleType("Insurer").RoleId).ConvertAll(x => {
                return new SelectListItem
                {
                    Text = x.FirstName + " " + x.LastName + "(" + x.UserId + ")",
                    Value = x.UserId.ToString()
                };
            });
            return View();
        }
        
        [HttpPost]
        public IActionResult Broker(BrokerBuyer model) {
            string brokerId = obj.BrokerIdByUserId(HttpContext.Session.GetString("UserName"));
            model.BrokerId = brokerId;
            model.PolicyStatus = 10;
            obj.AddAssetInBrokerBuyer(model);
            Feedback feedback = new Feedback() { UserId=brokerId.Split("_")[1],AssetId=model.AssetId,BuyerId=model.UserId,Response="Accepted by broker_1"};
            obj.AddInFeedback(feedback);
            Feedback feedback2 = new Feedback() { UserId = brokerId.Split("_")[1], AssetId = model.AssetId,BuyerId=model.UserId, Response = "Sent to insurer_1" };
            obj.AddInFeedback(feedback2);
            ViewBag.AllAssetOfVessel = obj.AllVeselAssetsByBrokerId("BR_" + brokerId);
            ViewBag.AllAsset = obj.GetAssets();
            ViewBag.AllBrokerBuyerAsset = obj.AllAssetOfBrokerBuyer();
            ViewBag.AllCountry = obj.GetCountries();
            ViewBag.AllInsurer = obj.GetAllUserFromRoleId(obj.GetRoleByRoleType("Insurer").RoleId).ConvertAll(x => {
                return new SelectListItem
                {
                    Text = x.FirstName + " " + x.LastName + "(" + x.UserId + ")",
                    Value = x.UserId.ToString()
                };
            });
            return View();
        }
        
        public IActionResult RemoveFromBuyerInsList(int AID,string UID)
        {
            string UserName = HttpContext.Session.GetString("UserName");
            if (UserName == null) return RedirectToAction("LoginPage");
            obj.DeleteAssetVesselBId(AID,UID);
            obj.DeleteFeedbackByUserIdAssetId(UID,AID);
            return RedirectToAction("BuyerPageAfterLogin");
        }
        
        public IActionResult Insurer()
        {
            string UserName = HttpContext.Session.GetString("UserName");
            if (UserName == null) return RedirectToAction("LoginPage");
            //ViewBag.AllAssetOfBuyerBroker= obj.AllAssetOfBrokerBuyer();
            List<BrokerBuyer> BrokerBuyerAsset= obj.GetAssetFromBrokerBuyerByInsurerId(UserName);
            List<BrokerBuyer> list = new List<BrokerBuyer>();
            obj.GetAllPolicy();
            foreach(var assets in BrokerBuyerAsset)
            {
                int id = assets.AssetId;
                string BuyerId=assets.UserId;
                if (obj.GetAssetIdFromPolicyDetailByBuyerIdAssetId(BuyerId, id) == null)
                    list.Add(assets);
            }
            ViewBag.AllAssetOfBuyerBroker = list;
            ViewBag.AllAsset = obj.GetAssets();
            ViewBag.AllUser = obj.AllUser();
            return View();
        }

        [Obsolete("use AcceptInsurance")]
        public IActionResult InsurerResponse(int AId,int Status,string ById){
            string UserName = HttpContext.Session.GetString("UserName");
            BrokerBuyer ExistingAsset= obj.GetAssetFromBrokerBuyerById(AId);
            InsurerBroker InsurerBroker = new InsurerBroker() { BrokerId=ExistingAsset.BrokerId,InsurerId="IR_"+UserName,AssetId=AId,BrokerageCharge=10000,BuyerId=ExistingAsset.UserId,InsuranceTenure=ExistingAsset.InsuranceTenure};
            obj.AddIntoInsurerBroker(InsurerBroker);
            InsurerBroker ExistingAsset2 = obj.GetAssetFromInsuerBrokerByBuyerIdAssetId(ExistingAsset.UserId,AId);
            string PolicyStatus;
            if (Status == 1) PolicyStatus = "Accepting your proposal for " + AId + " and will forward you all the details regarding insurance";
            else PolicyStatus = "Rejecting your proposal for asset: " + AId + " and will forward you all the details regarding rejection";
            PolicyDetail NewPolicy = new PolicyDetail(){Ibid=ExistingAsset2.Ibid,PolicyStatus=PolicyStatus,AssetId=AId,BuyerId=ExistingAsset.UserId};
            obj.AddInPolicy(NewPolicy);
            if (Status == 1)
            {
                //return RedirectToAction("AcceptInsurance");
                PolicyDetail ExistingPolicy = obj.GetAssetIdFromPolicyDetailByBuyerIdAssetId(ExistingAsset.UserId, AId);
                DateTime today = DateTime.Now;
                string StartDate = today.ToLongDateString();
                string EndDate = today.AddYears(InsurerBroker.InsuranceTenure).ToLongDateString();
                int PremAmt = 150000;
                PremiumAmountDetail premdetails = new PremiumAmountDetail() { Pid = ExistingPolicy.Pid, IntervalOfEmi = 6, StartDate = StartDate, EndDate = EndDate, DownPay = (float)(0.2 * PremAmt), PremAmt = PremAmt, Tenure = InsurerBroker.InsuranceTenure, PreType = "Default"};
                obj.AddInPremiumAmountDetails(premdetails);
            }
            return RedirectToAction("Insurer");
        }
        
        public IActionResult Signout()
        {
            string UserName = HttpContext.Session.GetString("UserName");
            if(UserName!=null)HttpContext.Session.SetString("UserName","null");
            HttpContext.Session.Clear();
            return RedirectToAction("LoginPage");
        }

        public IActionResult MoreDetail(int AId,string UID)
        {
            Asset assets= obj.GetAssetById(AId);
            string BrokerName,InsurerName=null;

            BuyerAssetVessel AssetofBuyerByAssetId = obj.getAssetOfVesselByUserIdAssetId(UID, AId);
            int Cid = (int)AssetofBuyerByAssetId.CountryId;
            string CountryName = obj.GetCountryByCoutryId(Cid).CountryName;
            string Broker_id=AssetofBuyerByAssetId.BrokerId;
            User CurrentBroker = obj.GetUserById((Broker_id.Split("_"))[1]);
            BrokerName = CurrentBroker.FirstName + " " + CurrentBroker.LastName+"("+Broker_id+")";
            InsurerBroker insurerbroker = obj.GetAssetFromInsuerBrokerByBuyerIdAssetId(UID, AId);
            if (insurerbroker!=null)
            {
                User currentInsurer = obj.GetUserById((insurerbroker.BrokerId.Split("_"))[1]);
                InsurerName = currentInsurer.FirstName + " " + currentInsurer.LastName;
            }
            Dictionary<string, List<string>> temp = new Dictionary<string, List<string>>();
            List<Feedback> feedback=obj.GetFeedbackByUserIdAssetId(UID,AId);
            foreach(var item in feedback)
            {
                string[] fd = item.Response.Split("_");
                if (temp.ContainsKey(fd[0])) temp[fd[0]].Add(fd[1]);
                else temp[fd[0]] = new List<string>() { fd[1] };
            }
            MoreDetail moredetails = new MoreDetail() { AssetName=assets.Name,AssetType=assets.Type,AssetInclusionDate="dummy",Broker=BrokerName,Insurer=InsurerName,CountryName=CountryName,status=temp};
            return View(moredetails);
        }
        
        public IActionResult AcceptInsurance(int AId,int Status,string ById)
        {
            string UserName = HttpContext.Session.GetString("UserName");

            if (Status == 1)
            {
                Asset asset = obj.GetAssetById(AId);
                Feedback feedback = new Feedback() { UserId = ById, AssetId = AId, BuyerId = ById, Response = "Accepted to insurer_1" };
                obj.AddInFeedback(feedback);
                BrokerBuyer BrokerBuyer = obj.GetAssetFromBrokerBuyerByBuyerIdAssetId(ById, AId);
                int CountryId=(int)obj.getAssetOfVesselByUserIdAssetId(ById,AId).CountryId;
                string CountryName= obj.GetCountryByCoutryId(CountryId).CountryName;
                User _broker = obj.GetUserById(BrokerBuyer.BrokerId.Split("_")[1]);
                User _buyer = obj.GetUserById(ById);
                InsurerBroker InsurerBroker = new InsurerBroker() { BrokerId = BrokerBuyer.BrokerId, InsurerId = "IR_" + UserName, AssetId = AId, BrokerageCharge = 10000, BuyerId = ById, InsuranceTenure = BrokerBuyer.InsuranceTenure };
                obj.AddIntoInsurerBroker(InsurerBroker);

                string BuyerName = _buyer.FirstName + " " + _buyer.LastName;
                string BrokerName = _broker.FirstName + " " + _broker.LastName; 
                DateTime today = DateTime.Now;
                string StartDate = today.ToLongDateString();
                string EndDate = today.AddYears(InsurerBroker.InsuranceTenure).ToLongDateString();
                
                InsurerResponse InsurerRes = new InsurerResponse()
                {
                    BuyerId = ById,
                    AssetId = AId,
                    AssetName = asset.Name,
                    AssetType = asset.Type,
                    StartDate = StartDate,
                    EndDate = EndDate,
                    BrokerName = BrokerName,
                    BuyerName = BuyerName,
                    CountryName=CountryName
                };
                return View(InsurerRes);
            }
            else
            {
                InsurerBroker ExistingAsset = obj.GetAssetFromInsuerBrokerByBuyerIdAssetId(ById, AId);
                PolicyDetail NewPolicy = new PolicyDetail() { Ibid = ExistingAsset.Ibid, PolicyStatus = "Rejected by insurer", AssetId = AId, BuyerId = ById };
                obj.AddInPolicy(NewPolicy);
            }
            return RedirectToAction("Insurer");
        }
        [HttpPost]
        public IActionResult AcceptInsurance(InsurerResponse _res)
        {
            string UserName = HttpContext.Session.GetString("UserName");
            InsurerBroker ExistingAsset = obj.GetAssetFromInsuerBrokerByBuyerIdAssetId(_res.BuyerId,_res.AssetId);
            PolicyDetail NewPolicy = new PolicyDetail() { Ibid = ExistingAsset.Ibid, PolicyStatus = _res.Feedback, AssetId = _res.AssetId, BuyerId = _res.BuyerId };
            obj.AddInPolicy(NewPolicy);
            PolicyDetail ExistingPolicy = obj.GetAssetIdFromPolicyDetailByBuyerIdAssetId(_res.BuyerId,_res.AssetId);
            PremiumAmountDetail premdetails = new PremiumAmountDetail() 
            { 
                Pid = ExistingPolicy.Pid, 
                IntervalOfEmi = _res.EmiInterval, 
                StartDate = _res.StartDate, 
                EndDate = _res.EndDate, 
                DownPay = _res.DownPay, 
                PremAmt = _res.PremiumAmount, 
                Tenure = ExistingAsset.InsuranceTenure, 
                PreType = _res.PremiumType 
            };
            obj.AddInPremiumAmountDetails(premdetails);

            List<BrokerBuyer> BrokerBuyerAsset = obj.GetAssetFromBrokerBuyerByInsurerId(UserName);
            List<BrokerBuyer> list = new List<BrokerBuyer>();
            obj.GetAllPolicy();
            foreach (var assets in BrokerBuyerAsset)
            {
                int id = assets.AssetId;
                string BuyerId = assets.UserId;
                if (obj.GetAssetIdFromPolicyDetailByBuyerIdAssetId(BuyerId, id) == null)
                    list.Add(assets);
            }
            ViewBag.AllAssetOfBuyerBroker = list;
            ViewBag.AllAsset = obj.GetAssets();
            ViewBag.AllUser = obj.AllUser();
            return View("Insurer");
        }

    }
}
