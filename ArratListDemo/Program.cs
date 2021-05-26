using System;
using System.Collections;
/// <summary>
/// 使用ArrayList来实现一个背包管理器，里面有出售物品和购买物品
/// 和展示物品的函数
/// </summary>

namespace ArratListDemo
{
    /// <summary>
    /// 背包管理器   管理物品的
    /// </summary>
    class BagMgr
    {
        //存储物品的容器
        private ArrayList items;
        //一共拥有多少钱
        private int money;


        public BagMgr(int money)
        {
            this.money = money;
            items = new ArrayList();

        }

        /// <summary>
        /// 买物品的函数
        /// </summary>
        /// <param name="item"></param>
        public void BuyItem(Item item)
        {
            if(item.money<0||item.num<=0)
            {
                Console.WriteLine("传入的物品的信息数据不合理。。。");
                return;

            }

            if(this.money<item.money*item.num)
            {
                Console.WriteLine("钱不够，买不起");
                return;
            }

            this.money -= item.money * item.num;

            Console.WriteLine("买了{0}，{1}个，花了{2}元钱",item.name,item.num, item.money * item.num);


            for(int i=0;i<items.Count;++i)
            {
                if((items[i] as Item).id==item.id)
                {
                    //数量加
                    (items[i] as Item).num += item.num;
                    return;

                }
            }

            items.Add(item);

        }

        /// <summary>
        /// 出售物品的函数
        /// </summary>
        /// <param name="item"></param>
        public void SellItem(Item item)
        {
            for(int i=0;i<items.Count;++i)
            {
                //如何判断卖的物品有没有
                //不能这么写，这么写有问题，
                //这么写的是在判断两个引用地址  是不是指向同一个房间，这样写有很大的问题
                //要注意
                //一般不是这样判断的，是根据唯一的id来判断的
                //if(items[i]==item)
                //{

                //}

                if((items[i] as Item).id==item.id)
                {
                    //卖的数量，还要判断
                    int num = 0;

                   
                    if((items[i] as Item).num>item.num)
                    { //1.比背包里面的少
                        num = item.num;
                    }

                    
                    else
                    {//2.比背包里面的多
                        num = (items[i] as Item).num;
                        //卖完了，就移除了
                        items.RemoveAt(i);

                    }

                    int sellMoney = (int)(num * (items[i] as Item).money * 0.8f);

                    this.money += sellMoney;

                    Console.WriteLine("卖了多少{0}钱", sellMoney);

                    return;

                }
            }

        }

       
        /// <summary>
        /// 根据id和数量来出售物品
        /// </summary>
        /// <param name="id"></param>
        /// <param name="num"></param>
        public void SellItem(int id,int num=1)
        {
            Item item = new Item(id,num);

            SellItem(item);

        }

        /// <summary>
        /// 展示背包里面还有多少个物品
        /// </summary>
      
        public void ShowItem()
        {
            for(int i=0;i<items.Count;++i)
            {
                Console.WriteLine("有{0}{1}个",(items[i] as Item).name,
                    (items[i] as Item).num);

            }

            Console.WriteLine("现在用有{0}钱", this.money);

        }

    }


    /// <summary>
    /// 物品类
    /// </summary>
    class Item
    {
        //物品的id
        public int id;
        //物品的单价
        public int money;
        //物品的名字
        public  string name;
        //物品的数量
        public int num;

        public Item(int id,int num)
        {
            this.id = id;
            this.num = num;

        }

        public Item(int id,int money,int num,string name)
        {
            this.id = id;
            this.money = money;
            this.num = num;
            this.name = name;

        }


    }


    class Program
    {
        static void Main(string[] args)
        {
            BagMgr bag = new BagMgr(9999);

            Item i1 = new Item(1, 10, 10, "红药");
            Item i2 = new Item(2, 20, 20, "蓝药");
            Item i3 = new Item(3, 999, 1, "屠龙刀");

            bag.BuyItem(i1);

            bag.BuyItem(i2);
            bag.BuyItem(i3);

            bag.ShowItem();


            Console.ReadKey();
        }
    }
}
