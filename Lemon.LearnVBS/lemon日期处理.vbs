
'dim fso 
'set fso = createobject("Scripting.FileSystemObject")

'msgbox Date() '日期函数返回值是一个string,但是是 2015/4/2这样的格式
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
