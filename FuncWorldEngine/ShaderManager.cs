using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

public static class ShaderManager
{
    static List<Shader> shaders = new List<Shader>();
    static ShaderNode root = new ShaderNode("root", -1);

    public static string shaderFolder = "";
    
    public static Shader getShaderHandle(List<string> shaderDescription)
    {
        ShaderNode current = root;
        ShaderNode next = null;
        
        foreach(string element in shaderDescription)
        {
            next = current.getChild(element);
            if(next == null)
            {
                next = new ShaderNode(element);
                current.children.Add(next);
            }
            current = next;
        }

        if(current.shader == -1)
        {
            current.shader = shaders.Count;
            shaders.Add(buildShader(shaderDescription));
        }

        return shaders[current.shader];
    }

    static Shader buildShader(List<string> description)
    {
        description.Sort();

        List<ShaderModule> mods = new List<ShaderModule>();        
        foreach(string mod in description)
        {
            try
            {
                mods.Add(new ShaderModule(mod));
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        string vertexVarString = "#version 440\n\n";
        string fragmentVarString = "#version 440\n\n";
        string vertexCode = "\n\nvoid main(void)\n{\n";
        string fragmentCode = "\n\nvoid main(void)\n{\n";

        List<string> vertexVars = new List<string>();
        List<string> fragmentVars = new List<string>();

        foreach (ShaderModule mod in mods)
        {
            foreach(string var in mod.vertexVars)
            {
                if(!vertexVars.Contains(var))
                {
                    vertexVars.Add(var);
                }
            }
            vertexCode += mod.vertexCode;
            foreach (string var in mod.fragmentVars)
            {
                if (!fragmentVars.Contains(var))
                {
                    fragmentVars.Add(var);
                }
            }
            fragmentCode += mod.fragmentCode;
        }

        foreach (string var in vertexVars)
        {
            vertexVarString += var + "\n";
        }
        foreach (string var in fragmentVars)
        {
            fragmentVarString += var + "\n";
        }

        vertexCode = vertexVarString + vertexCode + "\n}\n";
        fragmentCode = fragmentVarString + fragmentCode + "\n}\n";

        Console.WriteLine("vsh: \n{0}\n", vertexCode);
        Console.WriteLine("fsh: \n{0}\n", fragmentCode);

        return new Shader(vertexCode, fragmentCode);
    }
}

class ShaderNode
{
    public string name;
    public List<ShaderNode> children;
    public int shader;

    public ShaderNode(string name, int shader = -1)
    {
        this.name = name;
        children = new List<ShaderNode>();
        this.shader = shader;
    }

    public ShaderNode getChild(string name)
    {
        foreach(ShaderNode child in children)
        {
            if(child.name.Equals(name))
            {
                return child;
            }
        }

        return null;
    }
}

class ShaderModule
{
    public List<string> vertexVars;
    public List<string> fragmentVars;
    public string vertexCode;
    public string fragmentCode;

    public ShaderModule(string name)
    {
        vertexVars = new List<string>();
        fragmentVars = new List<string>();

        string file = ShaderManager.shaderFolder + name + ".xml";
        if(!File.Exists(file))
        {
            throw new Exception("Could not find xml: " + file);
        }

        string xmlstring = File.ReadAllText(file);
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(xmlstring);

        foreach (XmlNode node in xml.SelectNodes("/shader/vertex/vars/var"))
        {
            vertexVars.Add(node.InnerXml);
        }
        vertexCode = xml.SelectSingleNode("/shader/vertex/code").InnerXml;

        foreach (XmlNode node in xml.SelectNodes("/shader/frag/vars/var"))
        {
            fragmentVars.Add(node.InnerXml);
        }
        fragmentCode = xml.SelectSingleNode("/shader/frag/code").InnerXml;
    }
}