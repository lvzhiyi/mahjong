namespace basef.rule
{
    public class SelectRuleViewRadioProcess : SelectRuleRadioMouseClick
    {
        public const int JU_SHU = 0,
            FENG_DING = 1,
            PLAYER_COUNT = 2,
            DIAN_PAO = 3,
            WAN_FA = 4,
            TRUST = 5,
            TRUST_TIME = 6,
            TRUST_JIE_SAN = 7,
            ZI_MO = 8,//自摸
            DGH = 9,//点杠花
            REPLACE = 10,//换牌
            CHA_JIAO = 11,//查叫
            DI_FEN = 12,//底分
            INVERSION = 13,//翻型
            POINT_HU_START = 14,//几番起胡
            PIAO = 15,//选飘
            GAME_MODEL = 16,//游戏模式
            MAX_MULTIPLE = 17,//最大的倍数
            ORDER = 18,//顺序
            HANDSNUM = 19,//手牌数量
            FANG_SHU = 20,//麻将的房数
            DAFA = 21,//打法（软打硬打）
            JIESAN_COUNT_CURRENT_SCORE = 22,//提前解散是否计算当前分数
            JIESAN_TIME = 23,//解散时间
            RULE_WAFA = 24,//规则玩法，一个大规则中，需要分小的玩法，比如贵阳捉鸡
            REMAIN_BANKER = 25,//连庄
            MAKE_BANKER = 26,//首局做庄
        SALE_SCORE = 27,//卖分
            MUST_HU = 28,//必胡
        LUN_ZHUANG = 29,//轮庄
            BEI_FEN = 30,//倍分
            MULTIP_SCORE = 31,//炸弹加倍
            XUAN_FAN=32;//选番

        public int type;

        /// <summary>
        /// 预制需要绑定
        /// </summary>
        RulesView rulesView;

        public override void execute()
        {
            rulesView = transform.parent.parent.parent.parent.parent.GetComponent<RulesView>();
            RuleRadioBar bar = transform.GetComponent<RuleRadioBar>();
            int index = bar.index_space;
            if (bar.isLocked) return;
            if ((type == TRUST_TIME || type == TRUST_JIE_SAN) && rulesView.getRule().getTrusteeship() == -1)
            {
                return;
            }

            base.execute();

            switch (type)
            {
                case JU_SHU:
                    rulesView.getRule().matchCount = bar.count;
                    break;
                case FENG_DING:
                    rulesView.getRule().maxPoint = bar.count;
                    break;
                case PLAYER_COUNT:
                    rulesView.refreshPlayersNum(index);
                    break;
                case DIAN_PAO:
                    rulesView.getRule().setLuaRuleSingleNames(Rule.DIAO_PAO_TYPE, index);
                    break;
                case ZI_MO:
                    rulesView.getRule().setLuaRuleSingleNames(Rule.ZI_MO_ADDTION_TYPE, index);
                    break;
                case DGH:
                    rulesView.getRule().setLuaRuleSingleNames(Rule.KONG_ADDTING_TYPE, index);
                    break;
                case REPLACE:
                    rulesView.getRule().setLuaRuleSingleNames(Rule.REPLACE_COUNT_TYPE, index);
                    break;
                case CHA_JIAO:
                    rulesView.getRule().setLuaRuleSingleNames(Rule.CHA_JIAO_TYPE, index);
                    break;
                case INVERSION:
                    rulesView.getRule().setLuaRuleSingleNames(Rule.FAN_XING, index);
                    break;
                case POINT_HU_START:
                    rulesView.getRule().setLuaRuleSingleNames(Rule.CAN_HU_POINT, index);
                    break;
                case PIAO:
                    rulesView.getRule().setLuaRuleSingleNames(Rule.PIAO, index);
                    break;
                case DI_FEN:
                    rulesView.getRule().setBet(bar.count*100);
                    break;
                case WAN_FA:
                    break;
                case TRUST:
                    rulesView.selectTrustessShip(index, bar.count);
                    break;
                case TRUST_TIME:
                    rulesView.selectTrustessTime(index, bar.count);
                    break;
                case TRUST_JIE_SAN:
                    rulesView.selectTrustessJieSan(index, bar.count);
                    break;
                case GAME_MODEL:
                    rulesView.getRule().setLuaRuleSingleNames(Rule.MODEL_TYPE, index);
                    rulesView.refresh();
                    break;
                case MAX_MULTIPLE:
                    rulesView.getRule().setLuaRuleSingleNames(Rule.MAX_MULTIPLE, index);
                    break;
                case ORDER:
                    rulesView.getRule().setLuaRuleSingleNames(Rule.ORDER, index);
                    rulesView.refresh();
                    break;
                case HANDSNUM:
                    rulesView.getRule().setLuaRuleSingleNames(Rule.HANDSNUM, index);
                    rulesView.refresh();
                    break;
                case DAFA:
                    rulesView.refreshRuleDafa(index);
                    break;
                case JIESAN_COUNT_CURRENT_SCORE: //提前解散，是否记录当前的分数
                    rulesView.getRule().setRuleBoolAtribute("iscurrentscore", bar.index_space == 0 ? true : false);
                    break;
                case JIESAN_TIME: //提前解散,时间
                    rulesView.getRule().setRuleIntAtributeMethod("jiesantime", bar.count);
                    break;
                case FANG_SHU: //提前解散,时间
                    rulesView.getRule().setRuleIntAtributeMethod("fangshu", bar.count);
                    rulesView.refresh();
                    break;
                case RULE_WAFA: //设置规则的小规则,贵阳捉鸡

                    rulesView.getRule().setLuaRuleSingleNames(RULE_WAFA, bar.index_space);
                    rulesView.refresh();
                    break;
                case REMAIN_BANKER: //连庄
                    rulesView.getRule().setLuaRuleSingleNames(REMAIN_BANKER, bar.index_space);
                    break;
                case MAKE_BANKER: //首局做庄
                    rulesView.getRule().setLuaRuleSingleNames(MAKE_BANKER, bar.index_space);
                    break;
                case SALE_SCORE: //卖分
                    rulesView.getRule().setLuaRuleSingleNames(SALE_SCORE, bar.index_space);
                    break;
                case MUST_HU://必胡
                    rulesView.getRule().setLuaRuleSingleNames(MUST_HU, bar.index_space);
                    break;
                case LUN_ZHUANG://轮庄
                    rulesView.getRule().setLuaRuleSingleNames(LUN_ZHUANG, bar.index_space);
                    break;
                case BEI_FEN://倍分
                    rulesView.getRule().setLuaRuleSingleNames(BEI_FEN, bar.count);
                    break;
                case MULTIP_SCORE://炸弹分数
                    int score = 0;
                    if (bar.index_space == 1)
                        score = 5;
                    else if (bar.index_space==2)
                        score = 10;
                    rulesView.getRule().setLuaRuleSingleNames(MULTIP_SCORE, score);
                    break;
                case XUAN_FAN://选番
                    rulesView.getRule().setLuaRuleSingleNames(XUAN_FAN, bar.index_space);
                    break;

                    
            }
        }
    }
}
