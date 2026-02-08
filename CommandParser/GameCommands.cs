using betareborn.Blocks;
using betareborn.Entities;
using betareborn.Items;
using System.Reflection;

namespace betareborn.CommandParser;

public class GameCommands
{
    public static Dictionary<string, int> allItems = new();
    public static Dictionary<int, Block> blocks = new();
    public static Dictionary<int, Item> items = new();

    public GameCommands()
    {
        { // Get blocks
            var fields = typeof(Block).GetFields(BindingFlags.Static | BindingFlags.Public)
                .Where(f => f.FieldType == typeof(Block))
                .ToList();
            fields.ForEach(x =>
            {
                var block = ((Block)x.GetValue(null));
                var id = block.id;
                allItems.TryAdd(x.Name.ToLower(), id);
                blocks.TryAdd(id, block);
            });
        }


        { // Get items
            var fields = typeof(Item).GetFields(BindingFlags.Static | BindingFlags.Public)
                .Where(f => f.FieldType == typeof(Item))
                .ToList();
            fields.ForEach(x =>
            {
                var item = ((Item)x.GetValue(null));
                var id = item.id;
                allItems.TryAdd(x.Name.ToLower(), id);
                items.TryAdd(id, item);
            });
        }
    }

    [MinecraftCommand("clear", description: "clears inventory")]
    public void ClearInventory(CommandContext ctx)
    {
        var inventory = ctx.Game.player.inventory.mainInventory;
        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i] = null;
        }

        ctx.Reply("Inventory cleared.");
    }

    [MinecraftCommand("give", description: "gives items", usage: "/give <item> <optional number>")]
    public void GiveItem(CommandContext ctx, string itemName, int count = -1)
    {
        itemName = itemName.ToLower();
        if (allItems.TryGetValue(itemName, out var itemId))
        {
            int finalCount = count;

            if (count == -1) finalCount = items.TryGetValue(itemId, out var item) ? item.maxStackSize : 64;

            ctx.Game.player.inventory.addItemStackToInventory(new ItemStack(id: itemId, count: finalCount));
            ctx.Reply($"Gave {finalCount} of {itemName}");
        }
        else
        {
            ctx.Reply($"Item '{itemName}' not found.");
        }
    }


    [MinecraftCommand("heal", description: "heals player", usage: "/heal <optional number>")]
    public void Heal(CommandContext ctx, int amount = 20)
    {
        ctx.Game.player.heal(amount);
    }

    [MinecraftCommand("settime", aliases: "time", description: "sets time", usage: "/settime <sunrise|dawn|morning|noon|sunset|night|midnight>\n/settime <number>")]
    public void SetTime(CommandContext ctx, string timeValue)
    {
        long? timeToSet = timeValue.ToLower() switch
        {
            "sunrise" or "dawn" => 0,
            "morning" => 1000,
            "noon" or "day" => 6000,
            "sunset" or "dusk" => 12000,
            "night" => 13000,
            "midnight" => 18000,
            _ => long.TryParse(timeValue, out long t) ? t : null
        };

        if (timeToSet.HasValue)
        {
            ctx.Game.world.setWorldTime(timeToSet.Value);
            ctx.Reply($"Time set to {timeValue} ({timeToSet.Value})");
        }
        else
        {
            ctx.Reply($"Invalid time value: {timeValue}");
        }
    }

    [MinecraftCommand("teleport", aliases: "tp", description: "teleports player", usage: "/teleport <x> <y> <z>")]
    public void Teleport(CommandContext ctx, float x, float y, float z)
    {
        ctx.Game.player.setPosition(x, y, z);
    }

    [MinecraftCommand("summon", aliases: "spawn", description: "spawns mobs", usage: "/summon <entity name>")]
    public void Summon(CommandContext ctx, string name)
    {
        var p = ctx.Game.player;
        var ent = EntityRegistry.createEntityAt(name, ctx.Game.world, (float)p.posX, (float)p.posY, (float)p.posZ);

        if (ent == null)
        {
            Console.Error.WriteLine($"Entity created by createEntityInWorld is null `{name}`");
        }
    }

    [MinecraftCommand("weather", description: "sets weather", usage: "/weather <clear|rain|storm>")]
    public void Weather(CommandContext ctx, string command)
    {
        command = command.ToLower();
        switch (command)
        {
            case "clear":
                {
                    ctx.Game.world.weatherEffects.clear();
                    ctx.Game.world.getWorldInfo().setRaining(false);
                    ctx.Game.world.getWorldInfo().setThundering(false);
                    Console.WriteLine("Clear Weather");
                    break;
                }
            case "rain":
                {
                    ctx.Game.world.getWorldInfo().setRaining(true);
                    ctx.Game.world.getWorldInfo().setThundering(false);
                    break;
                }
            case "storm":
                {
                    ctx.Game.world.getWorldInfo().setRaining(true);
                    ctx.Game.world.getWorldInfo().setThundering(true);
                    break;
                }
        }
    }

    [MinecraftCommand("killall", description: "kills specifed mobs", usage: "/killall <all|living|monster|animal|item|tnt>\n/killall <entiny name>")]
    public void KillAll(CommandContext ctx, string filter = "all")
    {
        var world = ctx.Game.world;
        var entities = new List<Entity>(world.loadedEntityList);
        int count = 0;
        filter = filter.ToLower();

        foreach (var ent in entities)
        {
            if (ent is EntityPlayer) continue;

            bool shouldKill = filter switch
            {
                "all" => true,
                "living" or "mob" => ent is EntityLiving,
                "monster" => ent is EntityMob,
                "animal" => ent is EntityAnimal,
                "item" => ent is EntityItem,
                "tnt" => ent is EntityTNTPrimed,
                _ => EntityRegistry.getId(ent)?.Equals(filter, StringComparison.OrdinalIgnoreCase) ?? false
            };

            if (shouldKill)
            {
                world.setEntityDead(ent);
                count++;
            }
        }

        ctx.Reply($"Killed {count} entities (filter: {filter}).");
    }

    [MinecraftCommand("dis", description: "sets render distance", usage: "/dist <number>")]
    public void Distance(CommandContext ctx, int dist)
    {
        ctx.Game.gameSettings.renderDistance = dist;
    }

    [MinecraftCommand("help", description: "this command", usage: "/help <command>")]
    public void Help(CommandContext ctx, String command = "")
    {
        if (command == "")
        {
            // list commands
            string str = "Available commands:\n";
            foreach (var item in CommandService._commands)
            {
                var attribute = item.Value.GetCustomAttribute<MinecraftCommandAttribute>();
                if (attribute is null)
                {
                    continue;
                }
                if (attribute.Name == item.Key)
                {
                    str += $"/{item.Key}" + (attribute.Description != "" ? $" - {attribute.Description}" : "") + "\n";
                }
                else
                {
                    str += $"/{item.Key} (alias of {attribute.Name})\n";
                }
            }

            ctx.Reply(str);
            return;
        }
        else
        {
            // usage of specific command
            if (!CommandService._commands.TryGetValue(command, out MethodInfo? value))
            {
                ctx.Reply($"Cannot find command `{command}`");
                return;
            }
            var attribute = value.GetCustomAttribute<MinecraftCommandAttribute>();
            if (value is null || attribute is null)
            {
                ctx.Reply("Unknown error");
                return;
            }

            string str = $"/{attribute.Name}" + (attribute.Description != "" ? $" - {attribute.Description}" : "") + "\n";
            str += $"Usage:\n{attribute.Usage}";
            if (attribute.Aliases.Length > 0)
            {
                str += "\nAliases: " + String.Join(", ", attribute.Aliases);
            }

            ctx.Reply(str);
        }
    }
}