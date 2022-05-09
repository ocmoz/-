<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Electromechanics.aspx.cs" Inherits="Module_FM2E_Report_Output_Electromechanics" %>

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
						机电弱电信息月报
					</div>
				</div>
			</div>
			
			<div id="main-tablist">
				<div id="button-bar">
					<ul>
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 170px;cursor: hand;"
								onclick="javascript:jump('Jidian_Ruodian_SystemOperationMonthly_o.raq','')"
								value="系统运行情况表" />
						</li>
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 170px;cursor: hand;"
								onclick="javascript:jump('Jidian_Ruodian_SystemOuterRepairMonthly_o.raq','')"
								
								value="代维情况月报" />
						</li>
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 170px;cursor: hand;"
								onclick="javascript:jump('Jidian_Ruodian_SystemBasicData_o.raq','')"
								value="基础数据" />
						</li>
					</ul>
				</div>
			</div>
			<div id="table">
				<div id="ptk">
					<div id="tabtop-l"></div>
					<div id="tabtop-z">
						机电信息月报
					</div>
				</div>
			</div>
			<div id="main-tablist">
				<div id="button-bar">
					<ul>
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 170px;cursor: hand;"
								onclick="javascript:jump('Jidian_Jidian_MechElecInfoMonthly_o.raq','')"
								value="收费站机电信息月报" />
						</li>
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 170px;cursor: hand;"
								onclick="javascript:jump('Jidian_Jidian_MechElecInfoMonthlySummary_o.raq','')"
								value="机电信息月报汇总" />
						</li>
						
						
					</ul>
				</div>
			</div>
			</div>
	</body>
</html>

