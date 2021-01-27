using System.Collections.Generic;
using System.Linq;
using LeagueSharp;

namespace PROdiction
{
    public class DashEntry
    {
        public SpellSlot Slot;
        public string Champion;
    }

    public static class DashList
    {
        public static bool IsDashAvailable(Obj_AI_Hero target, float delay)
        {
            var spellSlots = DASHES.Where(entry => entry.Champion == target.ChampionName).Select(entry => entry.Slot)
                .ToList();

            var spellBook = target.Spellbook;

            foreach (var slot in spellSlots)
            {
                var cooldown = spellBook.GetSpell(slot).CooldownExpiresEx;
                if (cooldown <= 0.0f)
                {
                    return true;
                }

                if (delay > cooldown)
                {
                    return true;
                }
            }

            return false;
        }

        public static List<DashEntry> DASHES = new List<DashEntry>
        {
            new DashEntry {Champion = "Aatrox", Slot = SpellSlot.Q},
            new DashEntry {Champion = "Akali", Slot = SpellSlot.R},
            new DashEntry {Champion = "Alistar", Slot = SpellSlot.W},
            new DashEntry {Champion = "Caitlyn", Slot = SpellSlot.E},
            new DashEntry {Champion = "Camille", Slot = SpellSlot.E},
            new DashEntry {Champion = "Corki", Slot = SpellSlot.W},
            new DashEntry {Champion = "Fizz", Slot = SpellSlot.Q},
            new DashEntry {Champion = "Gragas", Slot = SpellSlot.E},
            new DashEntry {Champion = "Gnar", Slot = SpellSlot.E},
            new DashEntry {Champion = "Graves", Slot = SpellSlot.E},
            new DashEntry {Champion = "Irelia", Slot = SpellSlot.Q},
            new DashEntry {Champion = "Jax", Slot = SpellSlot.Q},
            new DashEntry {Champion = "Leblanc", Slot = SpellSlot.W},
            new DashEntry {Champion = "Leblanc", Slot = SpellSlot.R},
            new DashEntry {Champion = "LeeSin", Slot = SpellSlot.W},
            new DashEntry {Champion = "Lucian", Slot = SpellSlot.E},
            new DashEntry {Champion = "Khazix", Slot = SpellSlot.E},
            new DashEntry {Champion = "Nidalee", Slot = SpellSlot.W},
            new DashEntry {Champion = "Pantheon", Slot = SpellSlot.W},
            new DashEntry {Champion = "Riven", Slot = SpellSlot.Q},
            new DashEntry {Champion = "Riven", Slot = SpellSlot.E},
            new DashEntry {Champion = "Tristana", Slot = SpellSlot.W},
            new DashEntry {Champion = "Tryndamere", Slot = SpellSlot.E},
            new DashEntry {Champion = "Vayne", Slot = SpellSlot.Q},
            new DashEntry {Champion = "MonkeyKing", Slot = SpellSlot.E},
            new DashEntry {Champion = "Samira", Slot = SpellSlot.E},
            new DashEntry {Champion = "Ezreal", Slot = SpellSlot.E},
            new DashEntry {Champion = "Kassadin", Slot = SpellSlot.R},
            new DashEntry {Champion = "Katarina", Slot = SpellSlot.E},
            new DashEntry {Champion = "Shaco", Slot = SpellSlot.Q},
            new DashEntry {Champion = "Talon", Slot = SpellSlot.E},
            new DashEntry {Champion = "Elise", Slot = SpellSlot.E},
            new DashEntry {Champion = "Vladimir", Slot = SpellSlot.W},
            new DashEntry {Champion = "Fizz", Slot = SpellSlot.E},
            new DashEntry {Champion = "MasterYi", Slot = SpellSlot.Q},
            new DashEntry {Champion = "Yuumi", Slot = SpellSlot.W},
            new DashEntry {Champion = "Zed", Slot = SpellSlot.W},
            new DashEntry {Champion = "Yasuo", Slot = SpellSlot.E},
            new DashEntry {Champion = "Yone", Slot = SpellSlot.E},
        };
    }
}