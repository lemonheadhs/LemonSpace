
'CompressFiles
'先使用WinRAR压缩日志文件
'wshell.run("winrar a -ep -dw E:\入职工作日志\tt.rar E:\test\2015_*.txt")
	
	dim wshell,fso
	set wshell = createobject("wscript.shell")
	set fso = createobject("Scripting.FileSystemObject")

	'wshell.run "explorer E:\入职工作日志", 2
	'wshell.run "notepad E:\入职工作日志\2015_03-30.txt", 2
	wshell.run "C:\Users\Administrator\Desktop\HeidiSQL"



msgbox "success!"

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
