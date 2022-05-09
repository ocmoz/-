using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.BLL.Utils
{
    public class DepreciationMethod
    {
        public const short LINEAR_REDUCE = 1;

        /// <summary>
        /// 直线法折旧，按月折旧
        /// </summary>
        /// <param name="firstYear">投资开始年份</param>
        /// <param name="firstMonth">投资开始月份</param>
        /// <param name="currentYear">当前年</param>
        /// <param name="currentMonth">当前月</param>
        /// <param name="depreciableLife">折旧年限</param>
        /// <param name="price">初始价格</param>
        /// <param name="residualRate">残值率</param>
        /// <returns></returns>
        public static decimal GetResidualAssetByLinearReduce(int firstYear, int firstMonth,
            int currentYear,int currentMonth, int depreciableLife,
            decimal price, double residualRate)
        {
            decimal remainPrice = price * Convert.ToDecimal(residualRate);
            //不在折旧期内
            if(!IsBetweenXYears(firstYear, firstMonth, currentYear, currentMonth,depreciableLife))
            {
                if (DifferMonths(firstYear, firstMonth, currentYear, currentMonth) < 0)
                {//没有开始折旧
                    return price;
                }
                else
                {//折旧期已过
                    return remainPrice;
                }
            }            
            return price - (price-remainPrice) * DifferMonths(firstYear,
                firstMonth, currentYear, currentMonth) / depreciableLife / 12;
        }

        public static decimal GetResidualAssetByDoubleReduce(int firstYear, int firstMonth,
            int currentYear, int currentMonth, int depreciableLife,
            decimal price, double residualRate)
        {
            decimal remainPrice = price * Convert.ToDecimal(residualRate);
            //不在折旧期内
            if (!IsBetweenXYears(firstYear, firstMonth, currentYear, currentMonth, depreciableLife))
            {
                if (DifferMonths(firstYear, firstMonth, currentYear, currentMonth) < 0)
                {//没有开始折旧
                    return price;
                }
                else
                {//折旧期已过
                    return remainPrice;
                }
            }
            else if (depreciableLife <= 2)
            {//折旧时间在两年之内
                return GetResidualAssetByLinearReduce(firstYear, firstMonth,
                    currentYear, currentMonth, depreciableLife, price, residualRate);
            }
            else
            {
                int remainMonths = DifferMonths(firstYear, firstMonth, currentYear, currentMonth);
                int countMonth = 0;
                decimal answer = price;
                while (countMonth < (depreciableLife - 2) * 12 && remainMonths >= 12)
                {
                    answer -= answer * 2 / depreciableLife;
                    remainMonths -= 12;
                    countMonth += 12;
                }
                if (countMonth >= (depreciableLife - 2) * 12)
                {
                    answer -= (answer - remainPrice) * remainMonths / 24;
                }
                else
                {
                    answer -= answer * 2 / depreciableLife * remainMonths / 12;
                }
                return answer;
            }

        }

        private static bool IsBetweenXYears(int firstYear, int firstMonth,
            int currentYear, int currentMonth, int xYears)
        {
            int temp = DifferMonths(firstYear, firstMonth, currentYear, currentMonth);
            if(temp>=0 && temp<=xYears*12)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static int DifferMonths(int firstYear, int firstMonth,
            int currentYear, int currentMonth)
        {
            return (currentYear - firstYear) * 12 + currentMonth - firstMonth + 1;
        }
    }
}
