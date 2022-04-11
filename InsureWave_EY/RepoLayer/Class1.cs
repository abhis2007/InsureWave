using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepoLayer
{
    public class Class1 : IUser
    {
        InsureWaveContext db;
        public Class1()
        {
            db = new InsureWaveContext();
        }

        public void AddAssetInBrokerBuyer(BrokerBuyer asset)
        {
            db.BrokerBuyers.Add(asset);
            db.SaveChanges();
        }
        public List<BrokerBuyer> AllAssetOfBrokerBuyer() {
            return db.BrokerBuyers.ToList();
        }
        public void AddAssetInBuyerVessel(BuyerAssetVessel _Asset)
        {
            db.Add(_Asset);
            db.SaveChanges();
        }

        public void AddUser(User _user)
        {
            db.Users.Add(_user);
            db.SaveChanges();
        }

        public List<Gender> AllGender()
        {
            return db.Genders.ToList();
        }

        public List<Role> AllRole()
        {
            return db.Roles.ToList();
        }

        public List<User> AllUser()
        {
            return db.Users.ToList();
        }

        public List<BuyerAssetVessel> AllVesselAssets()
        {
            return db.BuyerAssetVessels.ToList();
        }

        public BuyerAssetVessel getAssetOfVesselById(int id)
        {
            return db.BuyerAssetVessels.Where(x=>x.AssetId==id).FirstOrDefault();
        }

        public BuyerAssetVessel getAssetOfVesselByUserIdAssetId(string _uid,int aid)
        {
            return db.BuyerAssetVessels.Where(x=>x.UserId==_uid && x.AssetId==aid).FirstOrDefault();
        }
        public List<Asset> GetAssets()
        {
            return db.Assets.ToList();
        }

        public List<BuyerAssetVessel> AllVeselAssetsByuserId(string uid)
        {
            return db.BuyerAssetVessels.Where(x=>x.UserId==uid).ToList();
        }

        public List<BuyerAssetVessel> AllVeselAssetsByBrokerId(string Brokerid)
        {
            return db.BuyerAssetVessels.Where(x => x.BrokerId == Brokerid).ToList();
        }

        public List<CountryCurrExchange> GetCountries()
        {
            return db.CountryCurrExchanges.ToList();
        }

        public List<Request> GetReqests()
        {
            return db.Requests.ToList();
        }

        public User GetUserById(string _UserId)
        {
            return db.Users.Where(val=>val.UserId==_UserId).FirstOrDefault();
            
        }
        public User GetUserByMail(string _mail)
        {
            return db.Users.Where(val => val.EmailId == _mail).FirstOrDefault();

        }
        public bool IsValid(string _uid, string _passwod)
        {
            return (db.Users.Where(value=>value.UserId==_uid && value.Password==_passwod).FirstOrDefault())!=null;
        }

        public string BrokerIdByUserId(string _uid)
        {
            Broker broker= db.Brokers.Where(x => x.UserId == _uid).FirstOrDefault();
            return broker.BrokerId;
        }

        public void AddBroker(Broker _brker)
        {
            db.Add(_brker);
            db.SaveChanges();
        }
        public List<Broker> AllBroker()
        {
            return db.Brokers.ToList();
        }

        public List<Request> AllRequest()
        {
            return db.Requests.ToList();
        }

        public void DeleteAssetVesselBId(int id,string uid)
        {
            db.Remove(getAssetOfVesselByUserIdAssetId(uid,id));
            db.SaveChanges();
        }
        public void UpdateAssetOfBuyerByAIdUId(int Aid, string Uid,BuyerAssetVessel _asset)
        {
            BuyerAssetVessel ExistingAsset = getAssetOfVesselByUserIdAssetId(Uid,Aid);
            ExistingAsset.CountryId = _asset.CountryId;
            ExistingAsset.RequestStatus = _asset.RequestStatus;
            ExistingAsset.BrokerId = _asset.BrokerId;
            db.SaveChanges();
        }

        public void AddInsurer(Insurer _insurer)
        {
            db.Add(_insurer);
            db.SaveChanges();
        }

        public List<InsurerBroker> AllAssetOfInsurerBroker()
        {
            return db.InsurerBrokers.ToList();
        }
        public BrokerBuyer GetAssetFromBrokerBuyerById(int AssetId)
        {
            return db.BrokerBuyers.Where(x=>x.AssetId==AssetId).FirstOrDefault();
        }

        public void AddIntoInsurerBroker(InsurerBroker _Instance)
        {
            db.Add(_Instance);
            db.SaveChanges();
        }

        public InsurerBroker GetAssetFromInsuerBrokerByBuyerIdAssetId(string BuyerId, int AssetId)
        {
            return db.InsurerBrokers.Where(x=>x.BuyerId==BuyerId && x.AssetId==AssetId).FirstOrDefault();
        }

        public void AddInPolicy(PolicyDetail policy)
        {
            db.Add(policy);
            db.SaveChanges();
        }

        public void AddInPremiumAmountDetails(PremiumAmountDetail _data)
        {
            db.Add(_data);
            db.SaveChanges();
        }

        public PolicyDetail GetAssetIdFromPolicyDetailByBuyerIdAssetId(string buyerid, int assetid)
        {
            return db.PolicyDetails.Where(x=>x.BuyerId==buyerid && x.AssetId==assetid).FirstOrDefault();
        }

        public BrokerBuyer GetAssetFromBrokerBuyerByBuyerIdAssetId(string bid, int aid)
        {
            return db.BrokerBuyers.Where(x=>x.UserId==bid && x.AssetId==aid).FirstOrDefault();
        }

        public Asset GetAssetById(int id)
        {
            return db.Assets.Where(x=>x.AssetId==id).FirstOrDefault();
        }

        public void AddInFeedback(Feedback f)
        {
            db.Add(f);
            db.SaveChanges();
        }

        public List<Feedback> GetFeedbackByUserIdAssetId(string uid, int Aid)
        {
            return db.Feedbacks.Where(x=>x.BuyerId==uid && x.AssetId==Aid).ToList();
        }

        public List<User> GetAllUserFromRoleId(int RoleId)
        {
            return db.Users.Where(x => x.RoleId == RoleId).ToList();
        }

        public Role GetRoleByRoleType(string role)
        {
            return db.Roles.Where(x => x.RoleName == role).FirstOrDefault();
        }

        public void DeleteFeedbackByUserIdAssetId(string Uid, int Aid)
        {
            List<Feedback> f = GetFeedbackByUserIdAssetId(Uid,Aid);
            foreach(var feed in f)
            {
                db.Remove(feed);
                db.SaveChanges();
            }
        }

        public CountryCurrExchange GetCountryByCoutryId(int id)
        {
            return db.CountryCurrExchanges.Where(x=>x.CountryId==id).FirstOrDefault();
        }

        public List<PolicyDetail> GetAllPolicy()
        {
            return db.PolicyDetails.ToList();
        }
        public PolicyDetail GetPolicyByAssetIdBuyerId(int Aid,string Bid)
        {
            return db.PolicyDetails.Where(x=>x.AssetId==Aid && x.BuyerId==Bid).FirstOrDefault();
        }

        public void DeleteFromBrokerBuyerByAssetIdBuyerId(int Aid, string BuyerId)
        {
            db.Remove(GetAssetFromBrokerBuyerByBuyerIdAssetId(BuyerId,Aid));
            db.SaveChanges();
        }

        public List<BrokerBuyer> GetAllAssetFromBrokerBuyerByBuyerIdAssetId(string bid, int aid)
        {
            return db.BrokerBuyers.Where(x=>x.AssetId==aid && x.BrokerId==bid).ToList();
        }

        public List<BrokerBuyer> GetAssetFromBrokerBuyerByInsurerId(string _insId)
        {
            return db.BrokerBuyers.Where(x => x.InsurerId == _insId).ToList();
        }
    }
}
