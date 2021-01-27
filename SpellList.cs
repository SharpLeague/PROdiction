using System.Collections.Generic;
using System.Linq;
using LeagueSharp;

namespace PROdiction
{
    public class SpellEntry
    {
        public string ChampionName;
        public string SpellName;
        public SpellSlot Slot;
        public int DangerLevel;
    }

    public class SpellList
    {
        public static int GetDangerLevel(string championName, SpellSlot slot)
        {
            var level = (from spell in SPELLS
                where spell.Slot == slot && spell.ChampionName == championName
                select spell.DangerLevel).FirstOrDefault();

            if (level == 0)
            {
                level = 2;
            }

            return level;
        }

        public static List<SpellEntry> SPELLS = new List<SpellEntry>
        {
            new SpellEntry {ChampionName = "Aatrox", SpellName = "AatroxQ1", Slot = SpellSlot.Q, DangerLevel = 3},
            new SpellEntry {ChampionName = "Pyke", SpellName = "PykeQRange", Slot = SpellSlot.Q, DangerLevel = 4},
            new SpellEntry {ChampionName = "Volibear", SpellName = "VolibearE", Slot = SpellSlot.E, DangerLevel = 2},
            new SpellEntry {ChampionName = "Urgot", SpellName = "UrgotQ", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Corki", SpellName = "MissileBarrage2", Slot = SpellSlot.R, DangerLevel = 2},
            new SpellEntry
                {ChampionName = "Cassiopeia", SpellName = "CassiopeiaR", Slot = SpellSlot.R, DangerLevel = 5},
            new SpellEntry
                {ChampionName = "Lux", SpellName = "LuxMaliceCannonMis", Slot = SpellSlot.R, DangerLevel = 5},
            new SpellEntry {ChampionName = "Senna", SpellName = "SennaR", Slot = SpellSlot.R, DangerLevel = 5},
            new SpellEntry {ChampionName = "Ekko", SpellName = "EkkoQ", Slot = SpellSlot.Q, DangerLevel = 4},
            new SpellEntry {ChampionName = "Senna", SpellName = "SennaW", Slot = SpellSlot.W, DangerLevel = 4},
            new SpellEntry {ChampionName = "Ekko", SpellName = "EkkoR", Slot = SpellSlot.R, DangerLevel = 3},
            new SpellEntry {ChampionName = "Pyke", SpellName = "PykeE", Slot = SpellSlot.E, DangerLevel = 3},
            new SpellEntry {ChampionName = "Draven", SpellName = "DravenRCast", Slot = SpellSlot.R, DangerLevel = 5},
            new SpellEntry {ChampionName = "Syndra", SpellName = "SyndraQ", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Zilean", SpellName = "ZileanQ", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Velkoz", SpellName = "VelkozE", Slot = SpellSlot.E, DangerLevel = 2},
            new SpellEntry {ChampionName = "Jayce", SpellName = "JayceQAccel", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Swain", SpellName = "SwainW", Slot = SpellSlot.W, DangerLevel = 3},
            new SpellEntry
                {ChampionName = "Veigar", SpellName = "VeigarBalefulStrike", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Evelynn", SpellName = "EvelynnR", Slot = SpellSlot.R, DangerLevel = 5},
            new SpellEntry {ChampionName = "Lissandra", SpellName = "LissandraE", Slot = SpellSlot.E, DangerLevel = 2},
            new SpellEntry {ChampionName = "Fiora", SpellName = "FioraW", Slot = SpellSlot.W, DangerLevel = 2},
            new SpellEntry {ChampionName = "Gragas", SpellName = "GragasQ", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Fizz", SpellName = "FizzR", Slot = SpellSlot.R, DangerLevel = 5},
            new SpellEntry {ChampionName = "Sion", SpellName = "SionE", Slot = SpellSlot.E, DangerLevel = 3},
            new SpellEntry {ChampionName = "Ezreal", SpellName = "EzrealR", Slot = SpellSlot.R, DangerLevel = 3},
            new SpellEntry
                {ChampionName = "NONE", SpellName = "KogMawLivingArtillery", Slot = SpellSlot.R, DangerLevel = 2},
            new SpellEntry
                {ChampionName = "Graves", SpellName = "GravesQLineSpell", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Karma", SpellName = "KarmaQMantra", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Bard", SpellName = "BardQ", Slot = SpellSlot.Q, DangerLevel = 3},
            new SpellEntry {ChampionName = "Khazix", SpellName = "KhazixE", Slot = SpellSlot.E, DangerLevel = 2},
            new SpellEntry {ChampionName = "Maokai", SpellName = "MaokaiQ", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Zyra", SpellName = "ZyraQ", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry
            {
                ChampionName = "JarvanIV", SpellName = "JarvanIVDemacianStandard", Slot = SpellSlot.E, DangerLevel = 2
            },
            new SpellEntry
                {ChampionName = "Karthus", SpellName = "KarthusLayWasteA2", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Poppy", SpellName = "PoppyQ", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Kassadin", SpellName = "RiftWalk", Slot = SpellSlot.R, DangerLevel = 2},
            new SpellEntry {ChampionName = "Neeko", SpellName = "NeekoE", Slot = SpellSlot.E, DangerLevel = 3},
            new SpellEntry
                {ChampionName = "Draven", SpellName = "DravenDoubleShot", Slot = SpellSlot.E, DangerLevel = 3},
            new SpellEntry {ChampionName = "Lillia", SpellName = "LilliaE2", Slot = SpellSlot.E, DangerLevel = 4},
            new SpellEntry {ChampionName = "Amumu", SpellName = "BandageToss", Slot = SpellSlot.Q, DangerLevel = 3},
            new SpellEntry {ChampionName = "Seraphine", SpellName = "SeraphineE", Slot = SpellSlot.E, DangerLevel = 3},
            new SpellEntry
                {ChampionName = "Caitlyn", SpellName = "CaitlynEntrapment", Slot = SpellSlot.E, DangerLevel = 1},
            new SpellEntry {ChampionName = "Annie", SpellName = "Incinerate", Slot = SpellSlot.W, DangerLevel = 2},
            new SpellEntry {ChampionName = "Syndra", SpellName = "syndrawcast", Slot = SpellSlot.W, DangerLevel = 2},
            new SpellEntry {ChampionName = "Yone", SpellName = "YoneR", Slot = SpellSlot.R, DangerLevel = 5},
            new SpellEntry {ChampionName = "Kayn", SpellName = "KaynAssW", Slot = SpellSlot.W, DangerLevel = 3},
            new SpellEntry {ChampionName = "Zoe", SpellName = "ZoeEBubble", Slot = SpellSlot.E, DangerLevel = 3},
            new SpellEntry
                {ChampionName = "Caitlyn", SpellName = "CaitlynYordleTrap", Slot = SpellSlot.W, DangerLevel = 2},
            new SpellEntry {ChampionName = "Xayah", SpellName = "XayahQ", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Illaoi", SpellName = "IllaoiE", Slot = SpellSlot.E, DangerLevel = 3},
            new SpellEntry {ChampionName = "Darius", SpellName = "DariusCleave", Slot = SpellSlot.Q, DangerLevel = 3},
            new SpellEntry {ChampionName = "Chogath", SpellName = "Rupture", Slot = SpellSlot.Q, DangerLevel = 3},
            new SpellEntry {ChampionName = "Nami", SpellName = "NamiR", Slot = SpellSlot.R, DangerLevel = 2},
            new SpellEntry {ChampionName = "Irelia", SpellName = "IreliaR", Slot = SpellSlot.R, DangerLevel = 5},
            new SpellEntry
                {ChampionName = "Nocturne", SpellName = "NocturneDuskbringer", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Taliyah", SpellName = "TaliyahQ", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Seraphine", SpellName = "SeraphineQ", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Quinn", SpellName = "QuinnQ", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Lux", SpellName = "LuxLightBinding", Slot = SpellSlot.Q, DangerLevel = 3},
            new SpellEntry {ChampionName = "Varus", SpellName = "VarusE", Slot = SpellSlot.E, DangerLevel = 2},
            new SpellEntry {ChampionName = "Pyke", SpellName = "PykeR", Slot = SpellSlot.R, DangerLevel = 5},
            new SpellEntry {ChampionName = "Ashe", SpellName = "Volley", Slot = SpellSlot.W, DangerLevel = 2},
            new SpellEntry {ChampionName = "Zac", SpellName = "ZacQ", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Olaf", SpellName = "OlafAxeThrowCast", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Zed", SpellName = "ZedW", Slot = SpellSlot.W, DangerLevel = 2},
            new SpellEntry
                {ChampionName = "Annie", SpellName = "InfernalGuardian", Slot = SpellSlot.R, DangerLevel = 5},
            new SpellEntry
                {ChampionName = "AurelionSol", SpellName = "AurelionSolQ", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Ekko", SpellName = "EkkoW", Slot = SpellSlot.W, DangerLevel = 3},
            new SpellEntry {ChampionName = "Jinx", SpellName = "JinxR", Slot = SpellSlot.R, DangerLevel = 5},
            new SpellEntry {ChampionName = "Gnar", SpellName = "GnarBigE", Slot = SpellSlot.E, DangerLevel = 2},
            new SpellEntry
                {ChampionName = "Orianna", SpellName = "OrianaDissonanceCommand", Slot = SpellSlot.W, DangerLevel = 2},
            new SpellEntry
                {ChampionName = "Veigar", SpellName = "VeigarEventHorizon", Slot = SpellSlot.E, DangerLevel = 3},
            new SpellEntry {ChampionName = "Khazix", SpellName = "KhazixW", Slot = SpellSlot.W, DangerLevel = 2},
            new SpellEntry {ChampionName = "Syndra", SpellName = "SyndraE", Slot = SpellSlot.E, DangerLevel = 2},
            new SpellEntry
            {
                ChampionName = "Caitlyn", SpellName = "CaitlynPiltoverPeacemaker", Slot = SpellSlot.Q, DangerLevel = 2
            },
            new SpellEntry {ChampionName = "Talon", SpellName = "TalonRakeReturn", Slot = SpellSlot.W, DangerLevel = 2},
            new SpellEntry {ChampionName = "Jhin", SpellName = "JhinW", Slot = SpellSlot.W, DangerLevel = 3},
            new SpellEntry {ChampionName = "Kled", SpellName = "KledE", Slot = SpellSlot.E, DangerLevel = 2},
            new SpellEntry
                {ChampionName = "Shyvana", SpellName = "shyvanafireballdragon2", Slot = SpellSlot.R, DangerLevel = 3},
            new SpellEntry {ChampionName = "Sett", SpellName = "SettW", Slot = SpellSlot.W, DangerLevel = 3},
            new SpellEntry
                {ChampionName = "Lux", SpellName = "LuxLightStrikeKugel", Slot = SpellSlot.E, DangerLevel = 2},
            new SpellEntry {ChampionName = "Sejuani", SpellName = "SejuaniR", Slot = SpellSlot.R, DangerLevel = 5},
            new SpellEntry {ChampionName = "Kayle", SpellName = "KayleQ", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Viktor", SpellName = "Laser", Slot = SpellSlot.E, DangerLevel = 2},
            new SpellEntry
                {ChampionName = "RekSai", SpellName = "reksaiqburrowed", Slot = SpellSlot.Q, DangerLevel = 3},
            new SpellEntry {ChampionName = "Gnar", SpellName = "GnarBigQ", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Leona", SpellName = "LeonaSolarFlare", Slot = SpellSlot.R, DangerLevel = 5},
            new SpellEntry {ChampionName = "Neeko", SpellName = "NeekoQ", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "TwistedFate", SpellName = "WildCards", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry
            {
                ChampionName = "Kennen", SpellName = "KennenShurikenHurlMissile1", Slot = SpellSlot.Q, DangerLevel = 2
            },
            new SpellEntry
            {
                ChampionName = "Veigar", SpellName = "VeigarDarkMatterCastLockout", Slot = SpellSlot.W, DangerLevel = 2
            },
            new SpellEntry {ChampionName = "Sion", SpellName = "SionR", Slot = SpellSlot.R, DangerLevel = 3},
            new SpellEntry
                {ChampionName = "Xerath", SpellName = "XerathArcanopulse2", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Janna", SpellName = "JannaQ", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Brand", SpellName = "BrandQ", Slot = SpellSlot.Q, DangerLevel = 3},
            new SpellEntry {ChampionName = "Varus", SpellName = "VarusQMissilee", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry
                {ChampionName = "Blitzcrank", SpellName = "StaticField", Slot = SpellSlot.R, DangerLevel = 2},
            new SpellEntry
                {ChampionName = "Graves", SpellName = "GravesChargeShot", Slot = SpellSlot.R, DangerLevel = 5},
            new SpellEntry {ChampionName = "Jinx", SpellName = "JinxWMissile", Slot = SpellSlot.W, DangerLevel = 3},
            new SpellEntry {ChampionName = "Anivia", SpellName = "FlashFrost", Slot = SpellSlot.Q, DangerLevel = 3},
            new SpellEntry
                {ChampionName = "Skarner", SpellName = "SkarnerFracture", Slot = SpellSlot.E, DangerLevel = 2},
            new SpellEntry {ChampionName = "Rumble", SpellName = "RumbleGrenade", Slot = SpellSlot.E, DangerLevel = 2},
            new SpellEntry
                {ChampionName = "Kalista", SpellName = "KalistaMysticShot", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Lucian", SpellName = "LucianQ", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Vi", SpellName = "Vi", Slot = SpellSlot.Q, DangerLevel = 3},
            new SpellEntry {ChampionName = "Sett", SpellName = "SettE", Slot = SpellSlot.E, DangerLevel = 4},
            new SpellEntry {ChampionName = "Illaoi", SpellName = "IllaoiR", Slot = SpellSlot.R, DangerLevel = 3},
            new SpellEntry {ChampionName = "Ziggs", SpellName = "ZiggsE", Slot = SpellSlot.E, DangerLevel = 2},
            new SpellEntry {ChampionName = "Leblanc", SpellName = "LeblancE", Slot = SpellSlot.E, DangerLevel = 3},
            new SpellEntry {ChampionName = "Leblanc", SpellName = "LeblancSlide", Slot = SpellSlot.W, DangerLevel = 2},
            new SpellEntry {ChampionName = "Irelia", SpellName = "IreliaE2", Slot = SpellSlot.E, DangerLevel = 4},
            new SpellEntry {ChampionName = "Kled", SpellName = "KledRiderQ", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry
                {ChampionName = "Zyra", SpellName = "zyrapassivedeathmanager", Slot = SpellSlot.E, DangerLevel = 3},
            new SpellEntry {ChampionName = "Ahri", SpellName = "AhriSeduce", Slot = SpellSlot.E, DangerLevel = 3},
            new SpellEntry {ChampionName = "Brand", SpellName = "BrandW", Slot = SpellSlot.W, DangerLevel = 2},
            new SpellEntry {ChampionName = "Yasuo", SpellName = "YasuoQ3", Slot = SpellSlot.Q, DangerLevel = 3},
            new SpellEntry {ChampionName = "Zed", SpellName = "ZedQ", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Senna", SpellName = "SennaQCast", Slot = SpellSlot.Q, DangerLevel = 3},
            new SpellEntry
                {ChampionName = "Rumble", SpellName = "RumbleCarpetBombM", Slot = SpellSlot.R, DangerLevel = 4},
            new SpellEntry {ChampionName = "Aatrox", SpellName = "AatroxW", Slot = SpellSlot.W, DangerLevel = 3},
            new SpellEntry {ChampionName = "Gnar", SpellName = "GnarR", Slot = SpellSlot.R, DangerLevel = 5},
            new SpellEntry {ChampionName = "Orianna", SpellName = "OriannasE", Slot = SpellSlot.E, DangerLevel = 2},
            new SpellEntry {ChampionName = "Lucian", SpellName = "LucianRMis", Slot = SpellSlot.R, DangerLevel = 2},
            new SpellEntry {ChampionName = "Urgot", SpellName = "UrgotE", Slot = SpellSlot.E, DangerLevel = 3},
            new SpellEntry {ChampionName = "Velkoz", SpellName = "VelkozW", Slot = SpellSlot.W, DangerLevel = 2},
            new SpellEntry {ChampionName = "Ezreal", SpellName = "EzrealQ", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry
                {ChampionName = "Xerath", SpellName = "XerathMageSpear", Slot = SpellSlot.E, DangerLevel = 2},
            new SpellEntry {ChampionName = "Yorick", SpellName = "YorickW", Slot = SpellSlot.W, DangerLevel = 2},
            new SpellEntry
                {ChampionName = "Xerath", SpellName = "XerathRMissileWrapper", Slot = SpellSlot.R, DangerLevel = 3},
            new SpellEntry
            {
                ChampionName = "DrMundo", SpellName = "InfectedCleaverMissileCast", Slot = SpellSlot.Q, DangerLevel = 3
            },
            new SpellEntry {ChampionName = "Qiyana", SpellName = "QiyanaR", Slot = SpellSlot.R, DangerLevel = 5},
            new SpellEntry {ChampionName = "Ezreal", SpellName = "EzrealW", Slot = SpellSlot.W, DangerLevel = 2},
            new SpellEntry {ChampionName = "Bard", SpellName = "BardR", Slot = SpellSlot.R, DangerLevel = 2},
            new SpellEntry
                {ChampionName = "AurelionSol", SpellName = "AurelionSolR", Slot = SpellSlot.R, DangerLevel = 3},
            new SpellEntry {ChampionName = "Ornn", SpellName = "OrnnRWave", Slot = SpellSlot.R, DangerLevel = 5},
            new SpellEntry
                {ChampionName = "Heimerdinger", SpellName = "Heimerdingerwm", Slot = SpellSlot.W, DangerLevel = 2},
            new SpellEntry {ChampionName = "Ahri", SpellName = "AhriOrbReturn", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry
                {ChampionName = "Heimerdinger", SpellName = "HeimerdingerE", Slot = SpellSlot.E, DangerLevel = 2},
            new SpellEntry {ChampionName = "NONE", SpellName = "KogMawVoidOoze", Slot = SpellSlot.E, DangerLevel = 2},
            new SpellEntry
                {ChampionName = "Mordekaiser", SpellName = "MordekaiserQ", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry
                {ChampionName = "Mordekaiser", SpellName = "MordekaiserE", Slot = SpellSlot.E, DangerLevel = 3},
            new SpellEntry {ChampionName = "Swain", SpellName = "SwainE", Slot = SpellSlot.E, DangerLevel = 3},
            new SpellEntry {ChampionName = "Lulu", SpellName = "LuluQPix", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Akali", SpellName = "AkaliE", Slot = SpellSlot.E, DangerLevel = 3},
            new SpellEntry {ChampionName = "Samira", SpellName = "SamiraQGun", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Braum", SpellName = "BraumRWrapper", Slot = SpellSlot.R, DangerLevel = 4},
            new SpellEntry {ChampionName = "Gragas", SpellName = "GragasR", Slot = SpellSlot.R, DangerLevel = 5},
            new SpellEntry
            {
                ChampionName = "Nautilus", SpellName = "NautilusAnchorDragMissile", Slot = SpellSlot.Q, DangerLevel = 3
            },
            new SpellEntry {ChampionName = "Gragas", SpellName = "GragasE", Slot = SpellSlot.E, DangerLevel = 2},
            new SpellEntry {ChampionName = "JarvanIV", SpellName = "JarvanIVEQ", Slot = SpellSlot.Q, DangerLevel = 3},
            new SpellEntry {ChampionName = "LeeSin", SpellName = "BlindMonkQOne", Slot = SpellSlot.Q, DangerLevel = 3},
            new SpellEntry {ChampionName = "Urgot", SpellName = "UrgotR", Slot = SpellSlot.R, DangerLevel = 5},
            new SpellEntry {ChampionName = "Corki", SpellName = "PhosphorusBomb", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry
                {ChampionName = "Twitch", SpellName = "TwitchVenomCask", Slot = SpellSlot.W, DangerLevel = 2},
            new SpellEntry {ChampionName = "Zoe", SpellName = "ZoeQ2", Slot = SpellSlot.Q, DangerLevel = 3},
            new SpellEntry {ChampionName = "Tristana", SpellName = "RocketJump", Slot = SpellSlot.W, DangerLevel = 2},
            new SpellEntry {ChampionName = "Ornn", SpellName = "OrnnQ", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Sona", SpellName = "SonaR", Slot = SpellSlot.R, DangerLevel = 5},
            new SpellEntry
                {ChampionName = "Ashe", SpellName = "EnchantedCrystalArrow", Slot = SpellSlot.R, DangerLevel = 5},
            new SpellEntry {ChampionName = "Lillia", SpellName = "LilliaW", Slot = SpellSlot.W, DangerLevel = 3},
            new SpellEntry
                {ChampionName = "Aphelios", SpellName = "ApheliosCalibrumQ", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Seraphine", SpellName = "SeraphineR", Slot = SpellSlot.R, DangerLevel = 5},
            new SpellEntry {ChampionName = "Braum", SpellName = "BraumQ", Slot = SpellSlot.Q, DangerLevel = 3},
            new SpellEntry {ChampionName = "Warwick", SpellName = "WarwickR", Slot = SpellSlot.R, DangerLevel = 5},
            new SpellEntry {ChampionName = "Ivern", SpellName = "IvernQ", Slot = SpellSlot.Q, DangerLevel = 3},
            new SpellEntry {ChampionName = "Varus", SpellName = "VarusR", Slot = SpellSlot.R, DangerLevel = 3},
            new SpellEntry {ChampionName = "Morgana", SpellName = "MorganaQ", Slot = SpellSlot.Q, DangerLevel = 3},
            new SpellEntry {ChampionName = "Taliyah", SpellName = "TaliyahW", Slot = SpellSlot.W, DangerLevel = 2},
            new SpellEntry {ChampionName = "Jhin", SpellName = "JhinRShot", Slot = SpellSlot.R, DangerLevel = 3},
            new SpellEntry {ChampionName = "Ryze", SpellName = "RyzeQ", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Ziggs", SpellName = "ZiggsQBounce2", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Riven", SpellName = "rivenizunablade", Slot = SpellSlot.R, DangerLevel = 5},
            new SpellEntry {ChampionName = "Elise", SpellName = "EliseHumanE", Slot = SpellSlot.E, DangerLevel = 4},
            new SpellEntry
                {ChampionName = "Shyvana", SpellName = "ShyvanaFireball", Slot = SpellSlot.E, DangerLevel = 2},
            new SpellEntry
                {ChampionName = "Leona", SpellName = "LeonaZenithBlade", Slot = SpellSlot.E, DangerLevel = 3},
            new SpellEntry {ChampionName = "Nami", SpellName = "NamiQ", Slot = SpellSlot.Q, DangerLevel = 3},
            new SpellEntry {ChampionName = "Sivir", SpellName = "SivirQ", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Qiyana", SpellName = "QiyanaQ_Water", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "NONE", SpellName = "KogMawQ", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Ziggs", SpellName = "ZiggsW", Slot = SpellSlot.W, DangerLevel = 2},
            new SpellEntry {ChampionName = "Ziggs", SpellName = "ZiggsR", Slot = SpellSlot.R, DangerLevel = 2},
            new SpellEntry {ChampionName = "Evelynn", SpellName = "EvelynnQ", Slot = SpellSlot.Q, DangerLevel = 3},
            new SpellEntry {ChampionName = "Orianna", SpellName = "OriannaQend", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Tryndamere", SpellName = "slashCast", Slot = SpellSlot.E, DangerLevel = 2},
            new SpellEntry {ChampionName = "Aphelios", SpellName = "ApheliosR", Slot = SpellSlot.R, DangerLevel = 5},
            new SpellEntry {ChampionName = "TahmKench", SpellName = "TahmKenchQ", Slot = SpellSlot.Q, DangerLevel = 3},
            new SpellEntry
                {ChampionName = "Lissandra", SpellName = "LissandraQShards", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry
                {ChampionName = "Leblanc", SpellName = "LeblancSoulShackleM", Slot = SpellSlot.R, DangerLevel = 3},
            new SpellEntry {ChampionName = "Taric", SpellName = "TaricE", Slot = SpellSlot.E, DangerLevel = 3},
            new SpellEntry {ChampionName = "Blitzcrank", SpellName = "RocketGrab", Slot = SpellSlot.Q, DangerLevel = 4},
            new SpellEntry {ChampionName = "Illaoi", SpellName = "IllaoiQ", Slot = SpellSlot.Q, DangerLevel = 3},
            new SpellEntry {ChampionName = "Sylas", SpellName = "SylasQLine", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Rakan", SpellName = "RakanQ", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Velkoz", SpellName = "VelkozQSplit", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Sion", SpellName = "SionQ", Slot = SpellSlot.Q, DangerLevel = 3},
            new SpellEntry {ChampionName = "Galio", SpellName = "GalioE", Slot = SpellSlot.E, DangerLevel = 3},
            new SpellEntry
                {ChampionName = "Orianna", SpellName = "OrianaDetonateCommand", Slot = SpellSlot.R, DangerLevel = 5},
            new SpellEntry
                {ChampionName = "Amumu", SpellName = "CurseoftheSadMummy", Slot = SpellSlot.R, DangerLevel = 5},
            new SpellEntry {ChampionName = "Nidalee", SpellName = "JavelinToss", Slot = SpellSlot.Q, DangerLevel = 3},
            new SpellEntry {ChampionName = "Kaisa", SpellName = "KaisaW", Slot = SpellSlot.W, DangerLevel = 3},
            new SpellEntry
                {ChampionName = "Cassiopeia", SpellName = "CassiopeiaQ", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Diana", SpellName = "DianaArcArc", Slot = SpellSlot.Q, DangerLevel = 3},
            new SpellEntry {ChampionName = "Sylas", SpellName = "SylasE2", Slot = SpellSlot.E, DangerLevel = 4},
            new SpellEntry {ChampionName = "Thresh", SpellName = "ThreshQ", Slot = SpellSlot.Q, DangerLevel = 3},
            new SpellEntry {ChampionName = "Zac", SpellName = "ZacE2", Slot = SpellSlot.E, DangerLevel = 3},
            new SpellEntry {ChampionName = "Yone", SpellName = "YoneQ3", Slot = SpellSlot.Q, DangerLevel = 4},
            new SpellEntry
                {ChampionName = "Darius", SpellName = "DariusAxeGrabCone", Slot = SpellSlot.E, DangerLevel = 3},
            new SpellEntry {ChampionName = "Soraka", SpellName = "SorakaQ", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Galio", SpellName = "GalioQ", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Poppy", SpellName = "PoppyRSpell", Slot = SpellSlot.R, DangerLevel = 3},
            new SpellEntry {ChampionName = "Thresh", SpellName = "ThreshEFlay", Slot = SpellSlot.E, DangerLevel = 3},
            new SpellEntry {ChampionName = "Yorick", SpellName = "YorickE", Slot = SpellSlot.E, DangerLevel = 2},
            new SpellEntry
                {ChampionName = "Pantheon", SpellName = "PantheonQMissile", Slot = SpellSlot.Q, DangerLevel = 3},
            new SpellEntry {ChampionName = "Rengar", SpellName = "RengarE", Slot = SpellSlot.E, DangerLevel = 3},
            new SpellEntry
                {ChampionName = "Xerath", SpellName = "XerathArcaneBarrage2", Slot = SpellSlot.W, DangerLevel = 2},
            new SpellEntry {ChampionName = "Gnar", SpellName = "GnarBigW", Slot = SpellSlot.W, DangerLevel = 2},
            new SpellEntry {ChampionName = "Malzahar", SpellName = "MalzaharQ", Slot = SpellSlot.Q, DangerLevel = 2},
            new SpellEntry {ChampionName = "Lucian", SpellName = "LucianW", Slot = SpellSlot.W, DangerLevel = 2},
        };
    }
}