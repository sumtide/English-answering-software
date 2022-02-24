using System;
using System.Collections.Generic;
using test;

public class MathTools
{

    /// <summary>
    /// 普通随机抽取
    /// </summary>
    /// <param name="rand"></param>
    /// <param name="datas"></param>
    /// <returns></returns>
    public static int[] RandomExtract(Random rand,int minValue,int maxValue,int returnCount)
    {
        List<int> result = new List<int>();
        if (rand != null)
        {
            for (int i = returnCount; i > 0;)
            {
                //获取datas集合中的一个随机整数
                int item = rand.Next(minValue,maxValue+1);
                if (result.Contains(item))
                    continue;
                else
                {
                    result.Add(item);
                    i--;
                }
            }
        }
        return result.ToArray();
    }

    /// <summary>
    /// 受控随机抽取
    /// </summary>
    /// <param name="rand"></param>
    /// <param name="datas"></param>
    /// <param name="weights"></param>
    /// <returns></returns>
    public static Word[] ControllerRandomExtract(Random rand, List<Word> datas, List<int> weights, int returnCount)
    {
        int nItemCount = datas.Count;

        List<Word> result = new List<Word>();

        if (rand != null)
        {
            //临时变量
            Dictionary<Word, int> dict = new Dictionary<Word, int>(nItemCount);
            List<KeyValuePair<Word, int>> listDict1 = new List<KeyValuePair<Word, int>>();
            //为每一项算一个随机数并乘相应的权值
            for (int i = 0; i < datas.Count; i++)
            {
                dict.Add(datas[i], rand.Next(10) * weights[i]);
            }
            listDict1.AddRange(dict);

            //根据权值对字典集合排序
            List<KeyValuePair<Word, int>> listDict = SortByValue(dict);

            //拷贝抽取权值最大值的前Count项
            foreach (KeyValuePair<Word, int> kvp in listDict.GetRange(0, returnCount))
            {
                result.Add(kvp.Key);
            }
        }
        return result.ToArray();
    }

    /// <summary>
    /// 排序集合
    /// </summary>
    /// <param name="dict"></param>
    /// <returns></returns>
    private static List<KeyValuePair<Word, int>> SortByValue(Dictionary<Word, int> dict)
    {
        List<KeyValuePair<Word, int>> list = new List<KeyValuePair<Word, int>>();

        if (dict != null)
        {
            list.AddRange(dict);

            list.Sort(
                delegate (KeyValuePair<Word, int> kvp1, KeyValuePair<Word, int> kvp2)
                {
                    return kvp2.Value - kvp1.Value;
                });
        }

        return list;
    }


    /// <summary>
    /// 获取比例
    /// </summary>
    public static float GetRate(float part, float total)
    {
        float result = 0;
        if (total != 0)
        {
            result = (part / total) * 100;
        }

        return float.Parse(result.ToString("00.00"));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mode"></param>
    /// <returns></returns>
    public static int[] GetRange(string mode)
    {
        List<int> temp = new List<int>();
        switch (mode)
        {
            case "四级必备词汇":
                temp.Add(1);
                temp.Add(4493);
                return temp.ToArray();

            case "四级救命词汇":
                temp.Add(4494);
                temp.Add(5179);
                return temp.ToArray();

            case "六级必备词汇":
                temp.Add(5180);
                temp.Add(7257);
                return temp.ToArray();

            case "六级救命词汇":
                temp.Add(7258);
                temp.Add(7749);
                return temp.ToArray();

            default:
                break;
        }
        return temp.ToArray();
    }

}

