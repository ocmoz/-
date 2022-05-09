<%@ page language="C#" autoeventwireup="true" inherits="Module_FM2E_Report_Output_Employee, App_Web_zdz9kcsv" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>报表管理</title>
		<meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
		<link href="../themes/StyleSheet.css" rel="stylesheet"
			type="text/css" />
		<script language="javascript" type="text/javascript">
		function jump(raq,station){
            var url = "http://";
		    url += "<%=quieeIP%>";
		    url += "/quiee/reportJsp/showReport.jsp"
            if (raq!="") url += "?raq=/" + raq;
            if ("<%=station%>"!="") url += "&station=" + "<%=station%>";
            window.open(url);
		}
        </script>
	</head>

	<body>
		<div id="main">
			<div id="table">
				<div id="ptk">
					<div id="tabtop-l"></div>
					<div id="tabtop-z">
						员工管理工作月报
					</div>
				</div>
			</div>
			
			<div id="main-tablist">
				<div id="button-bar">
					<ul>
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 200px;cursor: hand;"
								onclick="javascript:jump('Yuangong_EmployeeRelationship1.raq','')"
								value="员工管理月报表1" />
						</li>
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 200px;cursor: hand;"
								onclick="javascript:jump('Yuangong_EmployeeRelationship2.raq','')"
								value="员工管理月报表2" />
						</li>
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 200px;cursor: hand;"
								onclick="javascript:jump('Yuangong_EmployeeRelationship3.raq','')"
								value="员工管理月报表3" />
						</li>
						<br />
						<br />
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 200px;cursor: hand;"
								onclick="javascript:jump('Yuangong_EmployeeRelationship4.raq','')"
								value="离职员工管理月报表" />
						</li>
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 260px;cursor: hand;"
								onclick="javascript:jump('Yuangong_Shoufeihouqin_o.raq','')"
								value="收费后勤人员异动与劳动量数据统计表" />
						</li>
					</ul>
				</div>
			</div>
			<div id="table">
				<div id="ptk">
					<div id="tabtop-l"></div>
					<div id="tabtop-z">
						预算执行情况说明表
					</div>
				</div>
			</div>
			<div id="main-tablist">
				<div id="button-bar">
					<ul>
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 260px;cursor: hand;"
								onclick="javascript:jump('Yuangong_Yusuanzhixingqingkuang_o.raq','')"
								value="2012年预算执行情况说明（编制1682）" />
						</li>
						
					</ul>
				</div>
			</div>
			</div>
	</body>
</html>

