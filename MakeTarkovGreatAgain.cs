using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Models.Spt.Mod;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Services;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;

namespace MakeTarkovGreatAgain;

public record ModMetadata : AbstractModMetadata
{
    public override string ModGuid { get; init; } = "com.vinihns.maketarkovgreatagain";
    public override string Name { get; init; } = "Make Tarkov Great Again";
    public override string Author { get; init; } = "ViniHNS";
    public override SemanticVersioning.Version Version { get; init; } = new("1.0.1"); 
    public override SemanticVersioning.Range SptVersion { get; init; } = new("~4.0.0");
    public override string? License { get; init; } = "MIT";
    
    public override List<string>? Contributors { get; init; }
    public override List<string>? Incompatibilities { get; init; }
    public override Dictionary<string, SemanticVersioning.Range>? ModDependencies { get; init; }
    public override string? Url { get; init; } = "https://github.com/viniHNS/Make-Tarkov-Great-Again";
    public override bool? IsBundleMod { get; init; } = false;
}

[Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 1)]
public class Mod(
    ISptLogger<Mod> logger,
    DatabaseService databaseService)
    : IOnLoad
{
    // ID (TPL) da VPO-101
    private const string KRISS9MM_ID = "5fc3f2d5900b1d5091531e57";
    private const string ALPHADOG_SUPPRESSOR_ID = "5a33a8ebc4a282000c5a950d";
    private const string RPD_BARREL_520MM_ID = "6513eff1e06849f06c0957d4";
    private const string RPD_BARREL_SAWED_ID = "65266fd43341ed9aa903dd56";

    private const string PKM_BARREL_ID = "646371faf2404ab67905c8e9";

    private const string RSH12_ID = "633ec7c2a6918cb895019c6c";
    private const string CHIAPPA_9MM_ID = "624c2e8614da335f1e034d8c";
    private const string CHIAPPA_357_ID = "61a4c8884f95bc3b2c5dc96f";
    private const string MTS255_ID = "60db29ce99594040e04c4a27";
    private const string AA12_342MM_BARREL_ID = "670fd03dc424cf758f006946";
    private const string AA12_417MM_BARREL_ID = "670fd0a8d8d4eae4790c8187";

    private const string KEDR_THREADED_ADAPTER_ID = "57f3c7e024597738ea4ba286";

    
    public Task OnLoad()
    {
        logger.Info("[ViniHNS] Making Tarkov great again!");

        var items = databaseService.GetItems();
        
        ModifyKriss(items);
        ModifyAA12(items);
        ModifyRPD(items);
        UpdateAlphaDogSupressor(items);
        ModifyPKMBarrel(items);
        ModifyKEDR(items);

        RemoveDoubleActionPenalty(items, RSH12_ID);
        RemoveDoubleActionPenalty(items, CHIAPPA_9MM_ID);
        RemoveDoubleActionPenalty(items, CHIAPPA_357_ID);
        RemoveDoubleActionPenalty(items, MTS255_ID);

        return Task.CompletedTask;
    }

    private void ModifyKriss(Dictionary<MongoId, TemplateItem> items)
    {
        if (!items.TryGetValue(KRISS9MM_ID, out var kriss))
        {
            logger.Error($"Could not find Kriss Vector 9x19mm ({KRISS9MM_ID}).");
            return;
        }
        
        // Add fullauto fire mode, increase ergonomics and increase bfirerate
        if (kriss.Properties != null)
        {
            kriss.Properties.BFirerate = 1100; // 1100 RPM
        }
    }

    private void UpdateAlphaDogSupressor(Dictionary<MongoId, TemplateItem> items)
    {
        var newItems = new List<string> 
        {        
            "57ac965c24597706be5f975c",
            "57aca93d2459771f2c7e26db",
            "544a3f024bdc2d1d388b4568",
            "544a3a774bdc2d3a388b4567",
            "5d2dc3e548f035404a1a4798",
            "57adff4f24597737f373b6e6",
            "5c0517910db83400232ffee5",
            "591c4efa86f7741030027726",
            "570fd79bd2720bc7458b4583",
            "570fd6c2d2720bc6458b457f",
            "558022b54bdc2dac148b458d",
            "5c07dd120db834001c39092d",
            "5c0a2cec0db834001b7ce47d",
            "58491f3324597764bc48fa02",
            "584924ec24597768f12ae244",
            "5b30b0dc5acfc400153b7124",
            "6165ac8c290d254f5e6b2f6c",
            "60a23797a37c940de7062d02",
            "5d2da1e948f035477b1ce2ba",
            "5c0505e00db834001b735073",
            "609a63b6e2ff132951242d09",
            "584984812459776a704a82a6",
            "59f9d81586f7744c7506ee62",
            "570fd721d2720bc5458b4596",
            "57ae0171245977343c27bfcf",
            "5dfe6104585a0c3e995c7b82",
            "544a3d0a4bdc2d1b388b4567",
            "5d1b5e94d7ad1a2b865a96b0",
            "609bab8b455afd752b2e6138",
            "58d39d3d86f77445bb794ae7",
            "616554fe50224f204c1da2aa",
            "5c7d55f52e221644f31bff6a",
            "616584766ef05c2ce828ef57",
            "5b3b6dc75acfc47a8773fb1e",
            "615d8d878004cc50514c3233",
            "5b2389515acfc4771e1be0c0",
            "577d128124597739d65d0e56",
            "618b9643526131765025ab35",
            "618bab21526131765025ab3f",
            "5c86592b2e2216000e69e77c",
            "5a37ca54c4a282000d72296a",
            "5d0a29fed7ad1a002769ad08",
            "5c064c400db834001d23f468",
            "58d2664f86f7747fec5834f6",
            "57c69dd424597774c03b7bbc",
            "5b3b99265acfc4704b4a1afb",
            "5aa66a9be5b5b0214e506e89",
            "5aa66c72e5b5b00016327c93",
            "5c1cdd302e221602b3137250",
            "61714b2467085e45ef140b2c",
            "6171407e50224f204c1da3c5",
            "61713cc4d8e3106d9806c109",
            "5b31163c5acfc400153b71cb",
            "5a33b652c4a28232996e407c",
            "5a33b2c9c4a282000c5a9511",
            "59db7eed86f77461f8380365",
            "5a1ead28fcdbcb001912fa9f",
            "5dff77c759400025ea5150cf",
            "626bb8532c923541184624b4",
            "62811f461d5df4475f46a332",
            "63fc449f5bd61c6cf3784a88",
            "6477772ea8a38bb2050ed4db",
            "6478641c19d732620e045e17",
            "64785e7c19d732620e045e15",
            "65392f611406374f82152ba5",
            "653931da5db71d30ab1d6296",
            "655f13e0a246670fb0373245",
            "6567e751a715f85433025998",
            "6761759e7ee06333f108bf86",
            "67641a851b2899700609901a"
        };
        
        if (items.TryGetValue(ALPHADOG_SUPPRESSOR_ID, out var alphaDog))
        {
            if (alphaDog.Properties?.Slots != null)
            {
                var scopeSlot = alphaDog.Properties.Slots.FirstOrDefault(x => x.Name == "mod_scope");
                if (scopeSlot != null && scopeSlot.Properties?.Filters != null)
                {
                    var slotFilter = scopeSlot.Properties.Filters.FirstOrDefault();
                    if (slotFilter != null)
                    {
                        var filter = slotFilter.Filter;
                        foreach (var newItem in newItems)
                        {
                            filter.Add(new MongoId(newItem));
                        }
                    }
                }
            }
        }
    }

    private void ModifyAA12(Dictionary<MongoId, TemplateItem> items)
    {
        var newItems = new List<string> 
        { 
            "5c0111ab0db834001966914d",
            "560838c94bdc2d77798b4569",
            "5b363dea5acfc4771e1c5e7e"
        };

        void UpdateBarrel(string barrelId)
        {
            if (items.TryGetValue(barrelId, out var barrel))
            {
                if (barrel.Properties?.Slots != null)
                {
                    var muzzleSlot = barrel.Properties.Slots.FirstOrDefault(x => x.Name == "mod_muzzle");
                    if (muzzleSlot != null && muzzleSlot.Properties?.Filters != null)
                    {
                        var slotFilter = muzzleSlot.Properties.Filters.FirstOrDefault();
                        if (slotFilter != null)
                        {
                            var filter = slotFilter.Filter;
                            foreach (var newItem in newItems)
                            {
                                filter.Add(new MongoId(newItem));
                            }
                        }
                    }
                }
            }
        }

        UpdateBarrel(AA12_342MM_BARREL_ID);
        UpdateBarrel(AA12_417MM_BARREL_ID);
    }

    private void ModifyRPD(Dictionary<MongoId, TemplateItem> items)
    {
        var newItems = new List<string> 
        { 
            "59d64fc686f774171b243fe2",
            "5a0d716f1526d8000d26b1e2",
            "5f633f68f5750b524b45f112",
            "5c878ebb2e2216001219d48a",
            "59e61eb386f77440d64f5daf",
            "59e8a00d86f7742ad93b569c",
            "5a9ea27ca2750c00137fa672",
            "5cc9ad73d7f00c000e2579d4",
            "5c7951452e221644f31bfd5c",
            "615d8e9867085e45ef1409c6",
            "5a0abb6e1526d8000a025282",
            "59bffc1f86f77435b128b872",
            "593d489686f7745c6255d58a",
            "5a0d63621526d8dba31fe3bf",
            "5a9fbacda2750c00141e080f",
            "64942bfc6ee699f6890dff95"
        };

        void UpdateBarrel(string barrelId)
        {
            if (items.TryGetValue(barrelId, out var barrel))
            {
                if (barrel.Properties?.Slots != null)
                {
                    var muzzleSlot = barrel.Properties.Slots.FirstOrDefault(x => x.Name == "mod_muzzle");
                    if (muzzleSlot != null && muzzleSlot.Properties?.Filters != null)
                    {
                        var slotFilter = muzzleSlot.Properties.Filters.FirstOrDefault();
                        if (slotFilter != null)
                        {
                            var filter = slotFilter.Filter;
                            foreach (var newItem in newItems)
                            {
                                filter.Add(new MongoId(newItem));
                            }
                        }
                    }
                }
            }
        }

        UpdateBarrel(RPD_BARREL_520MM_ID);
        UpdateBarrel(RPD_BARREL_SAWED_ID);
    }

    private void ModifyPKMBarrel(Dictionary<MongoId, TemplateItem> items)
    {
        var newItems = new List<string> 
        { 
            "5c471bfc2e221602b21d4e17",
            "5bbdb83fd4351e44f824c44b",
            "5bc5a351d4351e003477a414",
            "5bc5a35cd4351e450201232f",
            "5cf79389d7f00c10941a0c4d",
            "5cf79599d7f00c10875d9212",
            "5cf67a1bd7f00c06585fb6f3",
            "5b86a0e586f7745b600ccb23"
        };

        if (items.TryGetValue(PKM_BARREL_ID, out var barrel))
        {
            if (barrel.Properties?.Slots != null)
            {
                var muzzleSlot = barrel.Properties.Slots.FirstOrDefault(x => x.Name == "mod_muzzle");
                if (muzzleSlot != null && muzzleSlot.Properties?.Filters != null)
                {
                    var slotFilter = muzzleSlot.Properties.Filters.FirstOrDefault();
                    if (slotFilter != null)
                    {
                        var filter = slotFilter.Filter;
                        foreach (var newItem in newItems)
                        {
                            filter.Add(new MongoId(newItem));
                        }
                    }
                }
            }
        }
    }

    private void ModifyKEDR(Dictionary<MongoId, TemplateItem> items)
    {
        var newItems = new List<string> 
        { 
            "5a7ad0c451dfba0013379712",
            "5a7037338dc32e000d46d257",
            "5a70366c8dc32e001207fb06",
            "5a705e128dc32e000d46d258",
            "5a7ad1fb51dfba0013379715",
            "5a6b585a8dc32e5a9c28b4f1",
            "5a6b592c8dc32e00094b97bf",
            "5a6b59a08dc32e000b452fb7",
            "5c7e8fab2e22165df16b889b",
            "5a33a8ebc4a282000c5a950d",
            "5c6165902e22160010261b28",
            "5a32a064c4a28200741e22de"
        };

        if (items.TryGetValue(KEDR_THREADED_ADAPTER_ID, out var item))
        {
            if (item.Properties?.Slots != null)
            {
                var muzzleSlot = item.Properties.Slots.FirstOrDefault(x => x.Name == "mod_muzzle");
                if (muzzleSlot != null && muzzleSlot.Properties?.Filters != null)
                {
                    var slotFilter = muzzleSlot.Properties.Filters.FirstOrDefault();
                    if (slotFilter != null)
                    {
                        var filter = slotFilter.Filter;
                        foreach (var newItem in newItems)
                        {
                            filter.Add(new MongoId(newItem));
                        }
                    }
                }
            }
        }
    }

    private void RemoveDoubleActionPenalty(Dictionary<MongoId, TemplateItem> items, string itemId)
    {
        if (items.TryGetValue(itemId, out var item))
        {
            if (item.Properties != null)
            {
                item.Properties.DoubleActionAccuracyPenalty = 0f;
            }
        }
        else
        {
            logger.Error($"Could not find item {itemId} to remove double action penalty.");
        }
    }
}