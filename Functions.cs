using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS8604 // 引用类型参数可能为 null。
namespace BRGenerator
{
    internal class Functions
    {
        public static void genMaterialItems(JObject jsonObject)
        {
            var materials = jsonObject[JsonHead.Materials].ToList();
            string assetsDir = Path.Combine(Directory.GetCurrentDirectory(), "assets/beyondreborn");
            string registryDir = Path.Combine(Directory.GetCurrentDirectory(), "java/registry");

            string modelDir = Path.Combine(assetsDir, "models/item");
            string textureDir = Path.Combine(assetsDir, "texture/item");
            string registryFile = Path.Combine(registryDir, "items.java(registry).txt");
            string memberFile = Path.Combine(registryDir, "item.java(member).txt");

            string registryWrite=string.Empty;
            string memberWrite=string.Empty;

            if (!Directory.Exists(modelDir)) Directory.CreateDirectory(modelDir);
            if (!Directory.Exists(textureDir)) Directory.CreateDirectory(textureDir);
            if (!Directory.Exists(registryDir)) Directory.CreateDirectory(registryDir);
            if (!File.Exists(registryFile)) File.Create(registryFile);
            if (!File.Exists(memberFile)) File.Create(memberFile);
            
            foreach (var material in materials)
            {
                registryWrite += string.Format("Registry.register(Registry.ITEM, new Identifier(\"beyondreborn\", \"{0}\"), {1}); \n",material.ToString(),material.ToString().ToUpper());
                memberWrite += string.Format("public static final MaterialItem {0} = new MaterialItem(baseSettings);\n",material.ToString().ToUpper());
            }
            File.WriteAllText(registryFile, registryWrite);
            File.WriteAllText(memberFile, memberWrite);
        }
    }
}
