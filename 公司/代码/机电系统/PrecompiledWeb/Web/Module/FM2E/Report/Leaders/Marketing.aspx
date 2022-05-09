<%@ page language="C#" autoeventwireup="true" inherits="Module_FM2E_Report_Output_Marketing, App_Web_gomuxzpn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>报表管理</title>
		<meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
		<link href="../themes/StyleSheet.css" rel="stylesheet"
			type="text/css" />
		<script language="javascript" type="text/javascript">
		function jump(raq,station){
            var url = "http://";
		    var ip1="<%=quieeIP1%>";
		    var ip2="<%=quieeIP2%>";
		    var tempurl =document.location.href;
		    if (tempurl.indexOf(ip2)>0) {
		        url += "<%=quieeIP1%>";
		    }
		    else{
		    	url += "<%=quieeIP%>";	    
		    }
		    url += "/quiee/reportJsp/showReport.jsp"
            if (raq!="") url += "?raq=/" + raq;
            if ("<%=station%>"!="") url += "&station=" + "<%=station%>";
            window.open(url);
		}
        </script>
	</head>

	<body>
		<div id="main" runat="server">
			<div id="table">
				<div id="ptk">
					<div id="tabtop-l"></div>
					<div id="tabtop-z">
						营销月报
					</div>
				</div>
			</div>
			
			<div id="main-tablist">
				<div id="button-bar">
					<ul id="yingxiao" runat="server">
						<li > 
							<input type="button" 
								onclick="javascript:jump('Yingxiao_Zhiguan_zonghejieguo_TrafficVolumeIncomeStatistical_o_o.raq','')"
								value="综合结果" style="font-family: 微软雅黑; height: 30px; width: 70px;cursor: hand;" />
						</li>
						<li>
							<input type="button" 
								onclick="javascript:jump('Yingxiao_Zhiguan_sumdongmi_TrafficVolumeIncomeStatistical_o.raq','')"
								value="报送董秘处" style="font-family: 微软雅黑; height: 30px; width: 70px;cursor: hand;" />
						</li>
						<li>
							<input type="button" 
								onclick="javascript:jump('Yingxiao_Teshu_sum_SpecialVehiclesClassifiedStatistics_o.raq','')"
								value="汇总" style="font-family: 微软雅黑; height: 30px; width: 70px;cursor: hand;" />
						</li>
						
					</ul>
				</div>
			</div>
			<div id="table">
				<div id="ptk">
					<div id="tabtop-l"></div>
					<div id="tabtop-z">
						员工关系
					</div>
				</div>
			</div>
			<div id="main-tablist">
				<div id="button-bar">
					<ul>
						
						<li>
							<input type="button" 
								onclick="javascript:jump('Yuangong_Yusuanzhixingqingkuang_o.raq','')"
								value="预算执行情况" style="font-family: 微软雅黑; height: 30px; width: 130px;cursor: hand;" />
						</li>
					</ul>
				</div>
			</div>
			
			<div id="table">
				<div id="ptk">
					<div id="tabtop-l"></div>
					<div id="tabtop-z">
						经营环境业务线月报
					</div>
				</div>
			</div>
			<div id="main-tablist">
				<div id="button-bar">
				<ul>
										
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 210px;cursor: hand;"
								onclick="javascript:jump('Jingying_balancebudgetaction_o.raq','')"
								value="部门预算执行情况月报" />
						</li>
					</ul>
				</div>
			</div>
			<div id="table">
				<div id="ptk">
					<div id="tabtop-l"></div>
					<div id="tabtop-z">
					    督导信息
					</div>
				</div>
			</div>
			<div id="main-tablist">
				<div id="button-bar">
				<ul>
										
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 210px;cursor: hand;"
								onclick="javascript:jump('t_balancebudgetaction_o.raq','')"
								value="督导模块" />
						</li>
					</ul>
				</div>
			</div>
			
			<div id="table">
				<div id="ptk">
					<div id="tabtop-l"></div>
					<div id="tabtop-z">
					    后勤月报
					</div>
				</div>
			</div>
			<div id="main-tablist">
				<div id="button-bar">
				<ul>
										
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 210px;cursor: hand;"
								onclick="javascript:jump('t_balancebudgetaction_o.raq','')"
								value="" />
						</li>
					</ul>
				</div>
			</div>
			
				<div id="table">
				<div id="ptk">
					<div id="tabtop-l"></div>
					<div id="tabtop-z">
					    机电月报
					</div>
				</div>
			</div>
			<div id="main-tablist">
				<div id="button-bar">
				<ul>
										
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 210px;cursor: hand;"
								onclick="javascript:jump('t_balancebudgetaction_o.raq','')"
								value="" />
						</li>
					</ul>
				</div>
			</div>
			
			<div id="table">
				<div id="ptk">
					<div id="tabtop-l"></div>
					<div id="tabtop-z">
					    路灯隧道
					</div>
				</div>
			</div>
			<div id="main-tablist">
				<div id="button-bar">
				<ul>
										
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 210px;cursor: hand;"
								onclick="javascript:jump('t_balancebudgetaction_o.raq','')"
								value="" />
						</li>
					</ul>
				</div>
			</div>
			
			<div id="table">
				<div id="ptk">
					<div id="tabtop-l"></div>
					<div id="tabtop-z">
					    客服中心
					</div>
				</div>
			</div>
			<div id="main-tablist">
				<div id="button-bar">
				<ul>
										
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 210px;cursor: hand;"
								onclick="javascript:jump('t_balancebudgetaction_o.raq','')"
								value="" />
						</li>
					</ul>
				</div>
			</div>
			
			</div>
			
	</body>
</html>

