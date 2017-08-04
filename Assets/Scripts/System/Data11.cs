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
        public NPC LaoHu;
        public float Door01L;
        public float Door01R;
        public float Door02L;
        public float Door02R;
        public GameObject Bowl;
        public GameObject Controller1;
        public GameObject Controller2;
        public StateChanger TV;
        public Shake YaoYi;
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
        }
        public override bool isTrigged(bool[] isDone, bool[] isDoing)
        {
            return true;
        }
        public override IEnumerator Excute()
        {
            DailyData11 data = GameSystem.dailyData11;
            GameSystem.isInteractingAllowed = false;
            yield return new WaitForSeconds(3f);
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
            yield return data.DouZi.walkTo(data.Door01L);
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
            return isDone[0] && HauntSystem.Num == 1 && GameSystem.isTimeIn(9, 10) && !isDoing[4];
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
            return (HauntSystem.Num == 2 && GameSystem.isTimeIn(0, 2)) || GameSystem.condition[2];
        }
        public override IEnumerator Excute()
        {
            DailyData11 data = GameSystem.dailyData11;
            if (GameSystem.condition[2])
            {
                TVTurnedOnTimes++;
                yield return SBreak();
                data.TV.isWork = true;
            }
            yield return SSpeak(data.LinJuZhangFu, "……唉，不需要", "不需要。", 2);
            yield return SSpeak(data.KeRen, "哎，你这就不懂了。算了算了，要不你", "再看看这个？", 2);
            yield return SSpeak(data.LinJuZhangFu, "好小子，", "好不容易回趟村，你就光顾着做你的生意了？", 2);
            yield return SSpeak(data.KeRen, "你这说的什么话？我在城里见了这么多稀奇，第一个想到你，", "一心只想着给你带回来。", 2);
            yield return SSpeak(data.LinJuZhangFu, "好好好，看看，看看。", "这是什么东西？", 2);
            yield return SSpeak(data.KeRen, "这叫八音盒，城里人穷讲究的东西。你看，", "照这儿一摁。", 6);
            PerformSystem.Play("8YinHe");
            yield return SSpeak(data.LinJuZhangFu, "", "这小娃娃哪里出来的？！", 2);
            yield return SSpeak(data.KeRen, "", "还能跳舞呢！", 2);
            yield return SSpeak(data.KeRen, "你孩子，", "你老头子，一定喜欢这小东西。", 2);
            yield return SSpeak(data.LinJuZhangFu, "嗨，说起", "老头子。", 2);
            yield return SSpeak(data.LinJuZhangFu, "", "上回阿宽给了我个小娃娃，也是这种，摁一下就能动的。", 2);
            yield return SSpeak(data.LinJuZhangFu, "我想拿它哄哄我们家老头子，结果，", "老头子说了啥你知道不知道？", 2);
            yield return SSpeak(data.KeRen, "", "说了啥？", 2);
            yield return SSpeak(data.LinJuZhangFu, "这个小娃娃不好。", "它只有一个动作。我的小娃娃长得比它好看，而且我让它怎么动，它就怎么动。", 2);
            yield return SSpeak(data.KeRen, "", "他也有这样的小娃娃？", 2);
            yield return SSpeak(data.LinJuZhangFu, "哪儿能呢？", "老头子说的是他的皮影。", 2);
            yield return SSpeak(data.KeRen, "", "唱戏的那个？", 2);
            yield return SSpeak(data.LinJuZhangFu, "", "还能是哪个？", 2);
            yield return SSpeak(data.KeRen, "我今早才路过戏院，", "还以为废了呢。", 2);
            yield return SSpeak(data.LinJuZhangFu, "早几年就说要废，可那个皮影戏班子一直不从。可不就一直拖着，", "拖到了今天。", 2);
            yield return SSpeak(data.KeRen, "", "皮影戏班子？", 2);
            yield return SSpeak(data.LinJuZhangFu, "现在也就剩四个人了。", "我家老头子，隔壁老王，还有老牛那两口子。", 2);
            yield return SSpeak(data.KeRen, "原来如此……诶，所以", "这八音盒你要吗？", 2);
            yield return Speak(data.LinJuZhangFu, "老头子一准不喜欢。", 2);
            while (TVTurnedOnTimes < 3)
            {
                if (GameSystem.condition[2])
                {
                    TVTurnedOnTimes++;
                    yield return SBreak();
                    data.TV.isWork = true;
                }
                yield return 1;
            }
        }

        int TVTurnedOnTimes = 0;
        IEnumerator SSpeak(NPC npc, string words1, string words2, float seconds)
        {
            npc.Speak(words1 + words2);
            int s = (int)(seconds * 10);
            for (int i = 0; i < s; i++)
            {
                if (GameSystem.condition[2])
                {
                    TVTurnedOnTimes++;
                    break;
                }
                else
                {
                    yield return new WaitForSeconds(0.1f);
                }
            }
            npc.ShutUp();
            if (GameSystem.condition[2])
            {
                yield return SBreak();
                yield return Speak(GameSystem.dailyData11.LinJuZhangFu, "……刚刚说到哪儿了？", 2);
                yield return Speak(npc, words2, seconds);
                GameSystem.dailyData11.TV.isWork = true;
            }

        }
        IEnumerator SBreak()
        {
            DailyData11 data = GameSystem.dailyData11;
            switch (TVTurnedOnTimes)
            {
                case 1:
                    data.LinJuZhangFu.Speak("咦？");
                    data.KeRen.Speak("咦？");
                    yield return new WaitForSeconds(2);
                    data.LinJuZhangFu.ShutUp();
                    data.KeRen.ShutUp();
                    yield return Speak(data.KeRen, "电视开了？", 2);
                    yield return Speak(data.LinJuZhangFu, "哪里碰到了吧。", 2);
                    //拿起遥控器
                    data.LinJuZhangFu.animator.SetBool("Pick", true);
                    PerformSystem.Hide(data.Controller1);
                    data.Controller2.SetActive(true);
                    PerformSystem.FadeIn(data.Controller2);
                    yield return new WaitForSeconds(0.5f);
                    //关电视
                    data.TV.ChangeState();
                    data.TV.isWork = false;
                    yield return new WaitForSeconds(0.5f);
                    //放下遥控器
                    data.LinJuZhangFu.animator.SetBool("Pick", false);
                    PerformSystem.Hide(data.Controller2);
                    PerformSystem.FadeIn(data.Controller1);

                    yield return Speak(data.KeRen, "诶，打开看看呗。", 2);
                    yield return Speak(data.LinJuZhangFu, "你不知道。在我们家，老头子在，电视就不能开。", 2);
                    yield return Speak(data.KeRen, "为啥？！", 2);
                    yield return Speak(data.LinJuZhangFu, "唉。别提了。", 2);
                    break;
                case 2:
                    yield return Speak(data.LinJuZhangFu, "今天是怎么了？", 2);
                    //拿起遥控器
                    data.LinJuZhangFu.animator.SetBool("Pick", true);
                    PerformSystem.Hide(data.Controller1);
                    PerformSystem.FadeIn(data.Controller2);
                    yield return new WaitForSeconds(0.5f);
                    //关电视
                    data.TV.ChangeState();
                    data.TV.isWork = false;
                    yield return new WaitForSeconds(0.5f);
                    //放下遥控器
                    data.LinJuZhangFu.animator.SetBool("Pick", false);
                    PerformSystem.Hide(data.Controller2);
                    PerformSystem.FadeIn(data.Controller1);
                    break;
                case 3:
                    //老胡从躺椅上下来
                    data.YaoYi.enabled = false;
                    data.LaoHu.transform.SetParent(null);
                    Quaternion ro = data.LaoHu.transform.rotation;
                    Vector3 po = data.LaoHu.transform.position;
                    data.LaoHu.transform.rotation = Quaternion.identity;
                    data.LaoHu.animator.SetBool("Lie", false);
                    //老胡走出来
                    yield return data.LaoHu.walkTo(data.Door02R);
                    PerformSystem.Hide(data.LaoHu.gameObject);
                    yield return new WaitForSeconds(0.5f);
                    yield return data.LaoHu.walkTo(data.Door02L);
                    PerformSystem.FadeIn(data.LaoHu.gameObject);
                    yield return data.LaoHu.walkTo(21);
                    //关电视
                    data.TV.ChangeState();
                    Destroy(data.TV);
                    //老胡回房
                    yield return data.LaoHu.walkTo(data.Door02L);
                    PerformSystem.Hide(data.LaoHu.gameObject);
                    yield return new WaitForSeconds(0.5f);
                    yield return data.LaoHu.walkTo(data.Door02R);
                    PerformSystem.FadeIn(data.LaoHu.gameObject);
                    yield return data.LaoHu.walkTo(po.x);
                    data.LaoHu.transform.rotation = ro;
                    data.LaoHu.transform.position = po;
                    data.LaoHu.Turn();
                    data.LaoHu.animator.SetBool("Lie", true);
                    data.LaoHu.transform.SetParent(data.YaoYi.transform);
                    data.YaoYi.enabled = true;

                    yield return Speak(data.KeRen, "……这？", 2);
                    yield return Speak(data.LinJuZhangFu, "老头子就是看不惯它。", 2);
                    yield return Speak(data.LinJuZhangFu, "他觉得这东西用了什么邪术，把人都勾跑了，然后就没人看他的戏了。", 2);
                    yield return Speak(data.LinJuZhangFu, "……他本来是想砸了这破玩意儿的。后来我告诉他这破玩意儿很贵，他又舍不得了……", 2);
                    break;
            }

        }
    }
}
