using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ParallelDemo
{
    public class TestList
    {
        /// <summary>
        /// 测试次数。
        /// </summary>
        private long testMaxRange = 10000;
        /// <summary>
        /// 构造数据Levels的总数。
        /// </summary>
        private long levelRange = 10000;

        public TestList() {
            GetLevels();
          //  GetLevelsWithYield();
        }

        /// <summary>
        /// 带yield
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Level> GetLevelsWithYield()
        {
            var level = new Level();
            for (var i = 0; i <= levelRange; i++)
            {
                level.Amount = i;
                level.ConditionAmount = i;
                level.Description = "Description" + i;
                level.Discount = i;
                level.FromMembership = true;
                level.IsDisabledApply = true;
                level.LevelId = i;
                level.LevelName = "LevelName" + i;
                level.LevelRank = i;
                level.OnSale = i;
                yield return level;
            };
        }
        /// <summary>
        /// 直接返回
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Level> GetLevels()
        {
            var level = new Level();
            var levels = new List<Level>();
            for (var i = 0; i <= levelRange; i++)
            {
                level = new Level();
                level.Amount = i;
                level.ConditionAmount = i;
                level.Description = "Description" + i;
                level.Discount = i;
                level.FromMembership = true;
                level.IsDisabledApply = true;
                level.LevelId = i;
                level.LevelName = "LevelName" + i;
                level.LevelRank = i;
                level.OnSale = i;
                levels.Add(level);
            };
            return levels;
        }
        /// <summary>
        /// 用count测试。
        /// </summary>
        public void TestCount()
        {
            //不带yield
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var levels = GetLevels();
            for (var i = 0; i <= testMaxRange; i++)
            {
                levels.Count();
            }
            stopwatch.Stop();
            Console.WriteLine($"\r\n\r\nTestCount-GetLevels耗时：{stopwatch.ElapsedMilliseconds}");

            //带yield
            stopwatch.Restart();
            var levelsTwo = GetLevelsWithYield();
            for (var i = 0; i <= testMaxRange; i++)
            {
                levelsTwo.Count();
            }
            stopwatch.Stop();
            Console.WriteLine($"TestCount-GetLevelsWithYield耗时：{stopwatch.ElapsedMilliseconds}");
        }
        /// <summary>
        /// 
        /// </summary>
        public void TestAny()
        {
            //不带yield
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var levels = GetLevels();
            for (var i = 0; i <= testMaxRange; i++)
            {
                levels.Any();
            }
            stopwatch.Stop();
            Console.WriteLine($"\r\nTestAny-GetLevels耗时：{stopwatch.ElapsedMilliseconds}");

            //带yield
            stopwatch.Restart();
            var levelsTwo = GetLevelsWithYield();
            for (var i = 0; i <= testMaxRange; i++)
            {
                levelsTwo.Any();
            }
            stopwatch.Stop();
            Console.WriteLine($"TestAny-GetLevelsWithYield耗时：{stopwatch.ElapsedMilliseconds}");



        }
    }

    public class Level
    {
        /// <summary>
        /// 会员类型Id
        /// </summary>
        public long LevelId { get; set; }

        /// <summary>
        /// 会员类型名称
        /// </summary>
        public string LevelName { get; set; }
        /// <summary>
        /// 类型条件金额
        /// </summary>
        public double ConditionAmount { get; set; }

        /// <summary>
        /// 会员类型顺序
        /// 用于标示会员类型的高低次序
        /// </summary>
        public int LevelRank { get; set; }


        /// <summary>
        /// 默认普通会员
        /// </summary>
        public const int DefaultLevelRank = 1;


        /// <summary>
        /// 类型说明
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        /// 类型折扣  (比如说九折就是0.9，数据库存的是double已经算好)
        /// </summary>
        public double? Discount { get; set; }

        /// <summary>
        /// 优惠多少元
        /// </summary>
        public double? OnSale { get; set; }


        /// <summary>
        /// 入会类型金额（入会充值多少钱满足什么类型）
        /// </summary>
        public double Amount { get; set; }


        /// <summary>
        /// 会员类型id关联
        /// </summary>
        public long MembershipGradeRuleId { get; set; }


        /// <summary>
        /// 是否禁用报名。
        /// </summary>
        public bool IsDisabledApply { get; set; }

        /// <summary>
        /// 是否自动升级
        /// </summary>
        public bool IsUpgrade { get; set; }

        /// <summary>
        /// 来源入会
        /// </summary>
        public bool FromMembership { get; set; }

    }
}
