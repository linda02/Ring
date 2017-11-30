using System;

namespace DotNet.Utility
{
    /// <summary>
    /// 使用Random类生成伪随机数
    /// </summary>
    public class RandomHelper
    {
        private static string randomStr = "0123456789abcdefghigklmnopqrstuvwxyzABCDEFGHIJKMLNOPQRSTUVWXYZ";
        //随机数对象
        private static Random random = new Random();


        #region 生成一个指定范围的随机整数
        /// <summary>
        /// 生成一个指定范围的随机整数，该随机数范围包括最小值，但不包括最大值
        /// </summary>
        /// <param name="minNum">最小值</param>
        /// <param name="maxNum">最大值</param>
        public static int GetRandomInt(int minNum, int maxNum)
        {
            return random.Next(minNum, maxNum);
        }
        #endregion

        #region 生成一个0.0到1.0的随机小数
        /// <summary>
        /// 生成一个0.0到1.0的随机小数
        /// </summary>
        public static double GetRandomDouble()
        {
            return random.NextDouble();
        }
        #endregion

        #region 产生随机字符（含大小写字母和数字）
        /// <summary>
        /// 产生随机字符（含大小写字母和数字）
        /// </summary>
        /// <param name="num">位数，默认为6</param>
        /// <returns></returns>
        public static string GetRandomString(int num = 6)
        {
            string returnValue = string.Empty;
            for (int i = 0; i < num; i++)
            {
                int r = random.Next(0, randomStr.Length - 1);
                returnValue += randomStr[r];
            }
            return returnValue;
        }
        #endregion

        #region 对一个数组进行随机排序
        /// <summary>
        /// 对一个数组进行随机排序
        /// </summary>
        /// <typeparam name="T">数组的类型</typeparam>
        /// <param name="arr">需要随机排序的数组</param>
        public static T[] GetRandomArray<T>(T[] arr)
        {
            //对数组进行随机排序的算法:随机选择一个位置，将当前位置上的值和它交换

            //交换的次数,这里使用数组的长度作为交换次数
            int count = arr.Length;
            //开始交换
            for (int i = 0; i < count; i++)
            {
                //生成随机数位置
                int index = GetRandomInt(0, arr.Length);
                //定义临时变量
                T temp;
                //交换两个随机数位置的值
                temp = arr[i];
                arr[i] = arr[index];
                arr[index] = temp;
            }
            return arr;
        }
        #endregion

    }
}
