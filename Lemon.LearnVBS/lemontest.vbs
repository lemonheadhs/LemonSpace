
dim fso 
set fso = createobject("Scripting.FileSystemObject")

dim tmpFilePath, targetPath, targetFullPath
tmpFilePath = "E:\��ְ������־\��������\Template.txt" 'ֵ���͸�ֵ����� set
targetPath = "E:\��ְ������־\"

dim wday
wday = Weekday(Date())
if wday=vbMonday then '��һ�����ܵ���־���ѹ��
	CompressFiles
end if

'�ж�Template.txt�Ƿ����
if (fso.FileExists(tmpFilePath)) then
	targetFullPath = targetPath & GetFormatedDateStr() & ".txt"
	'�ж�Ŀ���ļ�����û�е������־�ļ�
	if (fso.FileExists(targetFullPath))=false then
		fso.copyfile tmpFilePath, targetFullPath '�½���־�ļ�
		
	end if
end if
wshell.run "explorer E:\��ְ������־", 2
wshell.run targetFullPath, 2
wshell.run "C:\Users\Administrator\Desktop\�ܿ�����.exe - ��ݷ�ʽ"
wshell.run "C:\Users\Administrator\Desktop\ͨ��OA����"
wshell.run "C:\Users\Administrator\Desktop\FeiQ.exe - ��ݷ�ʽ"

dim tfile,tfolder
'fso.copyfile "E:\��ְ������־\��������\Template.txt", "E:\��ְ������־\111.txt"
'set tfile = fso.getfile("E:\WorkSpace\Demo\Template.txt") 'Ӣ��·����Ȼ��û���⣬���ľͲ��У�

'set tfile = fso.getfile("E:\��ְ������־\��������\Template.txt")
'set tfolder = fso.getfolder("E:\��ְ������־") 'ԭ���ǽű��ļ���������⣬�ĳ�ANSI�Ϳ�����

msgbox "success!"


function GetFormatedDateStr() 'as string ԭ��VBS���庯����ʱ����vb��Ҫָ����������
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
