
'CompressFiles
'��ʹ��WinRARѹ����־�ļ�
'wshell.run("winrar a -ep -dw E:\��ְ������־\tt.rar E:\test\2015_*.txt")
	
	dim wshell,fso
	set wshell = createobject("wscript.shell")
	set fso = createobject("Scripting.FileSystemObject")

	'wshell.run "explorer E:\��ְ������־", 2
	'wshell.run "notepad E:\��ְ������־\2015_03-30.txt", 2
	wshell.run "C:\Users\Administrator\Desktop\HeidiSQL"



msgbox "success!"

sub CompressFiles
	Const ForReading = 1, ForWriting = 2, ForAppending = 8
	dim wshell,fso
	set wshell = createobject("wscript.shell")
	set fso = createobject("Scripting.FileSystemObject")
	
	dim cfPath, rarParamStr
	cfPath = "E:\��ְ������־\��������\count.txt"
	
	'��ȡcount.txt�ļ��еļ���ֵ��Ȼ����ֵ������д���ļ�
	dim countFile,ts,weeksC
	set countFile = fso.getFile(cfPath)
	set ts = countFile.OpenAsTextStream(ForReading)
	weeksC = ts.ReadLine
	ts.Close
	rarParamStr = "winrar a -ep -dw E:\��ְ������־\14-63\��" & weeksC & "��.rar E:\��ְ������־\2015_*.txt"
	set ts = countFile.OpenAsTextStream(ForWriting)
	ts.Write weeksC+1
	ts.Close
	
	'ʹ��WinRARѹ����־�ļ�
	wshell.run(rarParamStr)

end sub
