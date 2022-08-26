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
        static string assetsDir = Path.Combine(Directory.GetCurrentDirectory(), "assets/beyondreborn");


        static string registryDir = Path.Combine(Directory.GetCurrentDirectory(), "java/registry");
        static string itemModelDir = Path.Combine(assetsDir, "models/item");
        static string itemTextureDir = Path.Combine(assetsDir, "texture/item");
        static string langDir = Path.Combine(assetsDir, "lang");

        static string registryFile = Path.Combine(registryDir, "items.java(registry).txt");
        static string memberFile = Path.Combine(registryDir, "item.java(member).txt");
        static string langFile = Path.Combine(langDir, "zh_cn.json");

        
        internal static void showHelp()
        {
            Console.WriteLine("BRGenerator materials : 生成items/material相关文件");
            Console.WriteLine("BRGenerator blocks : 生成blocks相关文件（未完成）");
        }

        public static void genMaterialItems(JObject jsonObject)
        {
            string registryWrite = string.Empty;
            string memberWrite = string.Empty;
            string modelWrite = string.Empty;

            var materials = jsonObject[JsonHead.Materials].ToList();
            checkAndFix();
            
            foreach (var material in materials)
            {
                registryWrite += string.Format("Registry.register(Registry.ITEM, new Identifier(\"beyondreborn\", \"{0}\"), {1}); \n",material.ToString(),material.ToString().ToUpper());
                memberWrite += string.Format("public static final MaterialItem {0} = new MaterialItem(materialSettings);\n", material.ToString().ToUpper());
                Console.WriteLine(string.Format("向{0}写入model信息", Path.Combine(itemModelDir, material.ToString() + ".json.txt")));
                modelWrite = string.Format("{{\n\"parent\": \"builtin/generated\",\n\"textures\": {{\n\"layer0\": \"beyondreborn:item/{0}\"\n}}\n}}\n",material.ToString());
                File.WriteAllText(Path.Combine(itemModelDir,material.ToString()+".json.txt"),modelWrite);
            }
            Console.WriteLine(string.Format("向{0}写入item注册信息:{1}",registryFile,registryWrite));
            File.WriteAllText(registryFile, registryWrite);
            Console.WriteLine(string.Format("向{0}写入item成员信息:{1}", memberFile,memberWrite));
            File.WriteAllText(memberFile, memberWrite);
            genLang(materials,"item");
        }

        private static void checkAndFix()
        {

            if (!Directory.Exists(itemModelDir)) Directory.CreateDirectory(itemModelDir);
            if (!Directory.Exists(itemTextureDir)) Directory.CreateDirectory(itemTextureDir);
            if (!Directory.Exists(registryDir)) Directory.CreateDirectory(registryDir);
            if(!Directory.Exists(langDir))Directory.CreateDirectory(langDir);

            if (!File.Exists(registryFile)) File.Create(registryFile);
            if (!File.Exists(memberFile)) File.Create(memberFile);
            if (!File.Exists(langFile)) File.Create(langFile);
        }

        private static void genLang(List<JToken> tockens,string type)
        {
            checkAndFix();
            string langWrite = string.Empty;
            Console.WriteLine(string.Format("写入语言文件：{0}",langFile));
            foreach (var tocken in tockens)
            {
                langWrite+= string.Format("\"{0}.beyondreborn.{1}\": \n", type,tocken.ToString());
                Console.WriteLine(langWrite);
            }
            File.WriteAllText(langFile, langWrite);
        }
    }
}
