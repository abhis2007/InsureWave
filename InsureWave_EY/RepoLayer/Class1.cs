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
            db.SaveChanges();
        }
    }
}
