
'dim fso 
'set fso = createobject("Scripting.FileSystemObject")

'msgbox Date() '���ں�������ֵ��һ��string,������ 2015/4/2�����ĸ�ʽ
'msgbox Time()

dim tstr
'tstr = GetFormatedDateStr()
'msgbox tstr

dim wday
wday = Weekday(Date())
if wday=vbThursday then
	msgbox "yeah!"
end if


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
