using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data11 : Data
{
    [System.Serializable]
    public struct DailyData11
    {
        public NPC LinJu;
        public NPC MuQin;
        public NPC DouZi;
        public NPC LinJuZhangFu;
        public NPC KeRen;
        public float Door01L;
        public float Door01R;
        public GameObject Bowl;
    }
    public DailyData11 data;
    private void Awake()
    {
        GameSystem.dailyData11 = data;
        MyEvents.Add(new MyEvent7(ref MyEvents));
    }

    /// <summary>
    /// 事件表列
    /// </summary>
    class MyEvent0 : MyEvent
    {
        public MyEvent0(ref List<MyEvent> list)
        {
            print("事件载入完毕...");
    //        print("!!!" + list.Count);
        }
        public override bool isTrigged(bool[] isDone, bool[] isDoing)
        {
            return true;
        }
        public override IEnumerator Excute()
        {
            DailyData11 data = GameSystem.dailyData11;
            GameSystem.isInteractingAllowed = false;
            yield return new WaitForSeconds(1.5f);
            yield return Speak(data.LinJu, "兰姐，你怎么来了？", 2);
            yield return Speak(data.MuQin, "刚炖的莲藕排骨汤，盛了碗热的来，给你尝尝。", 2);
            yield return Speak(data.LinJu, "哎哟，老早以前就馋着要喝你做的汤，求你求了这么久，可算是来了。", 2);

            yield return new WaitForSeconds(0.5f);
            data.LinJu.Turn();
            yield return new WaitForSeconds(0.5f);

            data.LinJu.Speak("豆子！你兰阿姨来了！过来打个招呼！");
            yield return new WaitForSeconds(0.3f);
            yield return data.DouZi.walkTo(data.Door01R);
            PerformSystem.Hide(data.DouZi.gameObject);
            yield return new WaitForSeconds(0.5f);
            data.LinJu.ShutUp();
            yield return  data.DouZi.walkTo(data.Door01L);
            PerformSystem.FadeIn(data.DouZi.gameObject);
            yield return data.DouZi.walkTo(2.26f);
            data.LinJu.Turn();
            yield return new WaitForSeconds(0.5f);

            yield return Speak(data.DouZi, "兰阿姨好！", 1);
            yield return Speak(data.MuQin, "豆子是不是又长高了呀。", 2);
            yield return Speak(data.LinJu, "小兔崽子一天到晚外面疯跑，能不长吗。", 2);
            yield return Speak(data.LinJu, "来，给兰阿姨接好，端去厨房，别跑，小心别撒了啊。", 2);


            PerformSystem.FocusOn(data.Bowl.transform);
            data.Bowl.transform.SetParent(data.DouZi.transform);
            data.MuQin.animator.SetBool("DuanWan", false);
            //这个po是调整碗的位置的
            Vector3 po = data.Bowl.transform.localPosition;
            po.y = 0.19f;
            data.Bowl.transform.localPosition = po;
            data.DouZi.animator.SetBool("DuanWan", true);
            yield return new WaitForSeconds(0.3f);
            po.x *= -1;
            data.Bowl.transform.localPosition = po;
            data.DouZi.Turn();
            yield return data.DouZi.walkTo(data.Door01L);
            PerformSystem.Hide(data.DouZi.gameObject);
            PerformSystem.Hide(data.Bowl);
            yield return new WaitForSeconds(0.5f);
            yield return data.DouZi.walkTo(data.Door01R);
            PerformSystem.FadeIn(data.DouZi.gameObject);
            PerformSystem.FadeIn(data.Bowl);
            GameSystem.isInteractingAllowed = true;
        }
    }

    class MyEvent1 : MyEvent
    {
        public MyEvent1(ref List<MyEvent> list)
        {
            list.Add(new MyEvent0(ref list));

        }
        public override bool isTrigged(bool[] isDone, bool[] isDoing)
        {
            return isDone[0] && HauntSystem.Num == 1 && GameSystem.isTimeIn(0, 2);
        }
        public override IEnumerator Excute()
        {
            DailyData11 data = GameSystem.dailyData11;
            yield return Speak(data.MuQin, "豆子越来越懂事了呀。", 6);
            yield return Speak(data.LinJu, "哪儿能？皮得很！我这是把他在家里关了几天，他才蔫了。", 3);
            yield return Speak(data.MuQin, "关他干嘛呀？男孩子小时候活泼点好。阿龙这个年纪的时候也是，在外面一玩一整天，叫都叫不回来。", 4);
            yield return Speak(data.LinJu, "哎！哪儿能和你家阿龙比！阿龙这么有出息，豆子差了远了！", 3);
        }
    }
    class MyEvent2 : MyEvent
    {
        public MyEvent2(ref List<MyEvent> list)
        {
            list.Add(new MyEvent1(ref list));

        }
        public override bool isTrigged(bool[] isDone, bool[] isDoing)
        {
            return isDone[0] && HauntSystem.Num == 1 && GameSystem.isTimeIn(2, 4) && !isDoing[1];
        }
        public override IEnumerator Excute()
        {
            DailyData11 data = GameSystem.dailyData11;
            yield return Speak(data.LinJu, "哎，我说。阿龙出去该有大半年了吧，怎么都没说回来看看？", 3);
            yield return Speak(data.LinJu, "哎哎哎，笑得这么幸福！", 1.5f);
            yield return Speak(data.LinJu, "是不是阿龙要回来啦？", 2);
            yield return Speak(data.MuQin, "嗯，今晚到。", 2);
            yield return Speak(data.LinJu, "唉哟，这么突然？", 2);
            yield return Speak(data.MuQin, "他早上才告诉我，我也吓了一跳。", 2);
            yield return Speak(data.LinJu, "哎嗨，可把你盼坏了！", 2);
        }
    }
    class MyEvent3 : MyEvent
    {
        public MyEvent3(ref List<MyEvent> list)
        {
            list.Add(new MyEvent2(ref list));
        }
        public override bool isTrigged(bool[] isDone, bool[] isDoing)
        {
            return isDone[0] && HauntSystem.Num == 1 && GameSystem.isTimeIn(5, 7) && !isDoing[2];
        }
        public override IEnumerator Excute()
        {
            DailyData11 data = GameSystem.dailyData11;
            yield return Speak(data.LinJu, "哎，不过这天看上去像是要下雨的样子啊。", 2);
            yield return Speak(data.LinJu, "从咱村到外面的那条路本来就又窄又陡。", 2);
            yield return Speak(data.LinJu, "万一再下雨一打滑……", 2);
            yield return Speak(data.MuQin, "我也是担心死了。", 2);
            if (HauntSystem.Num == 2) GameSystem.condition[1] = true;
            yield return Speak(data.MuQin, "我跟他说我去接他。结果倒是把他吓坏了。", 2);
            yield return Speak(data.MuQin, "给我做了老半天思想工作。后来我想也是，我去也是他的累赘，还要害他来照顾我。", 2);
            yield return Speak(data.LinJu, "哎呀，这不是担心你吗！", 2);
            yield return Speak(data.LinJu, "就是说嘛，这么大个人了，不用咱瞎操心了。", 2);
        }
    }
    class MyEvent4 : MyEvent
    {
        public MyEvent4(ref List<MyEvent> list)
        {
            list.Add(new MyEvent3(ref list));
        }
        public override bool isTrigged(bool[] isDone, bool[] isDoing)
        {
            return isDone[0] && HauntSystem.Num == 1 && GameSystem.isTimeIn(7, 9) && !isDoing[3];
        }
        public override IEnumerator Excute()
        {
            DailyData11 data = GameSystem.dailyData11;
            yield return Speak(data.MuQin, "我也是没有法子了，今早我还跑去龙王庙，求龙王爷显灵，保佑今晚不要下雨，让阿龙平平安安地回来。", 4);
            if (HauntSystem.Num == 2) GameSystem.condition[1] = true;
            yield return Speak(data.LinJu, "哎……放心吧放心吧，龙王爷会保佑你的！", 2);
        }
    }
    class MyEvent5 : MyEvent
    {
        public MyEvent5(ref List<MyEvent> list)
        {
            list.Add(new MyEvent4(ref list));
        }
        public override bool isTrigged(bool[] isDone, bool[] isDoing)
        {
            return isDone[0] && HauntSystem.Num == 1 && GameSystem.isTimeIn(9,10) && !isDoing[4];
        }
        public override IEnumerator Excute()
        {
            DailyData11 data = GameSystem.dailyData11;
            yield return Speak(data.LinJu, "当初他说要出去，你怎么都不拦一下呢？看你现在，人都瘦成这样了，还说不是想儿子想的。", 4);
            yield return Speak(data.MuQin, "能去城里学习是多大的福分，我拦了他，他以后要怪我的。", 2);
            yield return Speak(data.LinJu, "不想得慌啊？", 2);
            yield return Speak(data.MuQin, "平时也都还好，忙着忙着就忘了。", 2);
            yield return Speak(data.MuQin, "就是晚上难熬。一做梦就都是他。", 2);
        }
    }
    class MyEvent6 : MyEvent
    {
        public MyEvent6(ref List<MyEvent> list)
        {
            list.Add(new MyEvent5(ref list));
        }
        public override bool isTrigged(bool[] isDone, bool[] isDoing)
        {
            return isDone[0] && HauntSystem.Num == 1 && GameSystem.isTimeIn(10, 13) && !isDoing[5];
        }
        public override IEnumerator Excute()
        {
            DailyData11 data = GameSystem.dailyData11;
            yield return Speak(data.LinJu, "哎，我以后是绝对不把豆子放出去的。", 2);
            yield return Speak(data.MuQin, "你这——", 2);
            yield return Speak(data.LinJu, "这一是，那么久不见他，我是真受不了。二是，我不在他跟前管着他，他不知道要捅多大篓子！", 2);
            yield return Speak(data.MuQin, "豆子不是挺乖的吗？", 2);
            yield return Speak(data.LinJu, "你是不知道！他喜欢跟别人打架！", 2);
            yield return Speak(data.MuQin, "哎呀，男孩子嘛，难免的。", 2);
            yield return Speak(data.LinJu, "一两次都算了，他基本上出去一次就要跟别人打一架。他又倔，又不肯说为啥跟别人打！", 2);
            yield return Speak(data.MuQin, "哎呀，是不是受欺负了呀……", 2);
            yield return Speak(data.LinJu, "小兔崽子刚回来，我看他一层灰一层泥的，还心疼得不得了。跑去别人家里准备和他们理论理论，结果一看，哟呵。", 2);
            yield return Speak(data.MuQin, "嗯？", 2);
            yield return Speak(data.LinJu, "那家的比我们这祖宗还惨！哭哭啼啼的，脸上都挂了彩。", 2);
            yield return Speak(data.MuQin, "这……", 2);
            yield return Speak(data.LinJu, "谁不心疼自家娃呢？那家人也是把我好生一顿数落。", 2);
            yield return Speak(data.MuQin, "那边的小孩儿呢？说没说为啥要打架呀？", 2);
            yield return Speak(data.LinJu, "说啥呀！说是不知道怎么的了就突然惹到豆子了，冲过来就是一拳头。", 2);
            yield return Speak(data.LinJu, "我真是怕了他了。成天就担心他又在哪惹事了。", 2);
            yield return Speak(data.LinJu, "哎哎哎不说了不说了。", 2);
            yield return Speak(data.MuQin, "嗯你忙吧，我先回去了。", 2);
            yield return Speak(data.LinJu, "有空再来啊。", 2);
            data.MuQin.Turn();
            if (GameSystem.condition[0])
            {
                yield return Speak(data.LinJu, "哎等等！", 2);
                data.MuQin.Turn();
                yield return Speak(data.MuQin, "怎么了？", 1);
                yield return Speak(data.LinJu, "一个重要的事儿忘了说！", 2);
                yield return Speak(data.MuQin, "嗯？", 1);
                yield return Speak(data.LinJu, "佳佳啊，记得不？", 2);
                yield return Speak(data.MuQin, "那个眼睛挺大，挺干净的小姑娘？", 2);
                yield return Speak(data.LinJu, "是呀，你家阿龙不是还喜欢过人家吗？", 2);
                yield return Speak(data.LinJu, "我听说，只是听说啊。说他们马上要搬去城里了。", 2);
                yield return Speak(data.LinJu, "哎，你说佳佳她爸怎么就这么能耐呢？几年前才建了栋新楼，没住几年呢就又要搬。", 2);
                yield return Speak(data.LinJu, "你不让阿龙抓紧抓紧呀？这姑娘这么好的条件，再不抓紧人就跑啦！去找城里小伙子啦！", 3);
                yield return Speak(data.LinJu, "还是说——你不喜欢佳佳？", 2);
                yield return Speak(data.MuQin, "我倒不是……是我爸不喜欢她们家。", 2);
                yield return Speak(data.LinJu, "啊——也是。她们家就像是很瞧不起我们这小村子一样。建个楼吧，建呗，还专程建在这么远的地方，摆明了是要跟我们摆脱关系嘛。这下还跑更远了", 3);
                yield return Speak(data.MuQin, "我也管不了这么多。阿龙喜欢就好，不喜欢也", 2);
                yield return Speak(data.LinJu, "哎！也是！阿龙这么优秀的孩子，不愁找不到好姑娘的。", 2);
                yield return Speak(data.LinJu, "好啦好啦，回吧回吧，再不回汤都凉了。", 2);
                data.MuQin.Turn();
            }
            yield return new WaitForSeconds(0.5f);
            PerformSystem.Hide(data.MuQin.gameObject);
            data.LinJu.Turn();
            yield return data.LinJu.walkTo(data.Door01L);
            PerformSystem.Hide(data.LinJu.gameObject);
            yield return new WaitForSeconds(0.5f);
            yield return data.LinJu.walkTo(data.Door01R);
            PerformSystem.FadeIn(data.LinJu.gameObject);
            yield return data.LinJu.walkTo(16);
            PerformSystem.FadeOut(data.LinJu.gameObject);
        }
    }
    class MyEvent7 : MyEvent
    {
        public MyEvent7(ref List<MyEvent> list)
        {
            list.Add(new MyEvent6(ref list));
        }
        public override bool isTrigged(bool[] isDone, bool[] isDoing)
        {
            return HauntSystem.Num == 2 && GameSystem.isTimeIn(0, 2);
        }
        public override IEnumerator Excute()
        {
            DailyData11 data = GameSystem.dailyData11;
            yield return Speak(data.LinJuZhangFu, "……唉，不需要不需要。", 6);
            yield return Speak(data.KeRen, "哎，你这就不懂了。算了算了，要不你再看看这个？", 2);
            yield return Speak(data.LinJuZhangFu, "好小子，好不容易回趟村，你就光顾着做你的生意了？", 2);
            yield return Speak(data.KeRen, "你这说的什么话？我在城里见了这么多稀奇，第一个想到你，一心只想着给你带回来。", 2);
            yield return Speak(data.LinJuZhangFu, "好好好，看看，看看。这是什么东西？", 2);
            yield return Speak(data.KeRen, "这叫八音盒，城里人穷讲究的东西。你看，照这儿一摁。", 2);
            PerformSystem.Play("8YinHe");
            yield return Speak(data.LinJuZhangFu, "……唉，不需要不需要。", 2);
            yield return Speak(data.KeRen, "哎，你这就不懂了。算了算了，要不你再看看这个？", 2);
            yield return Speak(data.KeRen, "哎，你这就不懂了。算了算了，要不你再看看这个？", 2);
            yield return Speak(data.LinJuZhangFu, "……唉，不需要不需要。", 2);
            yield return Speak(data.LinJuZhangFu, "……唉，不需要不需要。", 2);
            yield return Speak(data.LinJuZhangFu, "……唉，不需要不需要。", 2);

        }
    }
}
