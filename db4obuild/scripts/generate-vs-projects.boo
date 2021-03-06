import Useful.BooTemplate from Boo.Lang.Useful

import System
import System.IO

class ProjectTemplate(AbstractTemplate):
	public projectName as string
	public outputFileName as string
	
def compile(fname as string) as ProjectTemplate:
	compiler = TemplateCompiler(TemplateBaseClass: ProjectTemplate)
	compiler.DefaultImports.Add("System")
	result = compiler.CompileFile(fname)
	assert 0 == len(result.Errors), result.Errors.ToString()
	compiledType = result.GeneratedAssembly.GetType(compiler.TemplateClassName)
	return compiledType()
	
if len(argv) < 1:
	print "generate-vs-project <PROJECT NAME> [TARGET PLATFORM]"
	return
	
if len(argv) == 1:
	projectName, = argv
	target = "*.csproj"
else:
	projectName, target= argv

outputFolder = "../../db4o.net/${projectName}"
Directory.CreateDirectory(outputFolder) if not Directory.Exists(outputFolder) 

for templateName in Directory.GetFiles("templates", target):
	template = compile(templateName)
	template.projectName = projectName
	template.Output = StringWriter()
	template.Execute()
	generatedFile = Path.Combine(outputFolder, template.outputFileName)
	print generatedFile
	File.WriteAllText(generatedFile, template.Output.ToString())
	