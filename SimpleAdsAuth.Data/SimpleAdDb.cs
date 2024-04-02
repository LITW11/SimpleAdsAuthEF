using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace SimpleAdsAuth.Data
{
    public class SimpleAdDb
    {
        private readonly string _connectionString;

        public SimpleAdDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddSimpleAd(SimpleAd ad)
        {
            using var ctx = new SimpleAdsDataContext(_connectionString);
            ctx.SimpleAds.Add(ad);
            ctx.SaveChanges();
        }

        public List<SimpleAd> GetAds()
        {
            using var ctx = new SimpleAdsDataContext(_connectionString);
            return ctx.SimpleAds.Include(a => a.User).ToList();
        }

        public List<SimpleAd> GetAdsForUser(int userId)
        {
            using var ctx = new SimpleAdsDataContext(_connectionString);
            return ctx.Users.Include(u => u.SimpleAds).FirstOrDefault(u => u.Id == userId).SimpleAds;
        }

        public int GetUserIdForAd(int adId)
        {
            using var ctx = new SimpleAdsDataContext(_connectionString);
            return ctx.SimpleAds.FirstOrDefault(i => i.Id ==adId).UserId;
        }

        public void Delete(int id)
        {
            using var ctx = new SimpleAdsDataContext(_connectionString);
            ctx.Database.ExecuteSqlInterpolated($"DELETE FROM SimpleAds WHERE Id = {id}");
        }
    }
}