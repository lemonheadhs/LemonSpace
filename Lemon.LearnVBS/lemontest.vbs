
dim fso 
set fso = createobject("Scripting.FileSystemObject")

dim tmpFilePath, targetPath, targetFullPath
tmpFilePath = "E:\入职工作日志\交接资料\Template.txt" '值类型赋值不需加 set
targetPath = "E:\入职工作日志\"

dim wday
wday = Weekday(Date())
if wday=vbMonday then '周一将上周的日志打包压缩
	CompressFiles
end if

'判断Template.txt是否存在
if (fso.FileExists(tmpFilePath)) then
	targetFullPath = targetPath & GetFormatedDateStr() & ".txt"
	'判断目的文件夹下没有当天的日志文件
	if (fso.FileExists(targetFullPath))=false then
		fso.copyfile tmpFilePath, targetFullPath '新建日志文件
		
	end if
end if
wshell.run "explorer E:\入职工作日志", 2
wshell.run targetFullPath, 2
wshell.run "C:\Users\Administrator\Desktop\管控助手.exe - 快捷方式"
wshell.run "C:\Users\Administrator\Desktop\通达OA精灵"
wshell.run "C:\Users\Administrator\Desktop\FeiQ.exe - 快捷方式"

dim tfile,tfolder
'fso.copyfile "E:\入职工作日志\交接资料\Template.txt", "E:\入职工作日志\111.txt"
'set tfile = fso.getfile("E:\WorkSpace\Demo\Template.txt") '英文路径居然就没问题，中文就不行？

'set tfile = fso.getfile("E:\入职工作日志\交接资料\Template.txt")
'set tfolder = fso.getfolder("E:\入职工作日志") '原来是脚本文件编码的问题，改成ANSI就可以了

msgbox "success!"


function GetFormatedDateStr() 'as string 原来VBS定义函数的时候不像vb需要指定返回类型
	dim strYear,strMonth,strDay
	strYear = "" & Year(Date())
	strMonth = "" & Month(Date())
	if len(strMonth)=1 then
		strMonth = "0" & strMonth
	end if
	strDay = "" & Day(Date())
	if len(strDay)=1 then
		strDay = "0" & strDay
	end if
	GetFormatedDateStr = strYear & "_" & strMonth & "-" & strDay
end function

sub CompressFiles
	Const ForReading = 1, ForWriting = 2, ForAppending = 8
	dim wshell,fso
	set wshell = createobject("wscript.shell")
	set fso = createobject("Scripting.FileSystemObject")
	
	dim cfPath, rarParamStr
	cfPath = "E:\入职工作日志\交接资料\count.txt"
	
	'读取count.txt文件中的计数值，然后将数值递增后写回文件
	dim countFile,ts,weeksC
	set countFile = fso.getFile(cfPath)
	set ts = countFile.OpenAsTextStream(ForReading)
	weeksC = ts.ReadLine
	ts.Close
	rarParamStr = "winrar a -ep -dw E:\入职工作日志\14-63\第" & weeksC & "周.rar E:\入职工作日志\2015_*.txt"
	set ts = countFile.OpenAsTextStream(ForWriting)
	ts.Write weeksC+1
	ts.Close
	
	'使用WinRAR压缩日志文件
	wshell.run(rarParamStr)

end sub
