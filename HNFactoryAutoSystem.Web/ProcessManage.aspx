﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessManage.aspx.cs" Inherits="HNFactoryAutoSystem.Web.ProcessManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>工艺流程管理</title>
    <link href="Scripts/ace_admin_cn/assets/css/bootstrap.min.css" rel="stylesheet" />
    <%--<link href="Scripts/bootstrap-4.4.1/css/bootstrap.min.css" rel="stylesheet" />--%>
    <link href="Scripts/ace_admin_cn/assets/css/font-awesome.min.css" rel="stylesheet" />
    <script src="Scripts/ace_admin_cn/assets/js/jquery.min.js"></script>

    <script src="Scripts/bootstrap-table/bootstrap-table.js"></script>
    <link href="Scripts/bootstrap-table/bootstrap-table.min.css" rel="stylesheet" />
    <script src="Scripts/bootstrap-table/locale/bootstrap-table-zh-CN.js"></script>

    <link rel="stylesheet" href="Scripts/ace_admin_cn/assets/css/ace.min.css" />
    <link rel="stylesheet" href="Scripts/ace_admin_cn/assets/css/ace-rtl.min.css" />
    <link rel="stylesheet" href="Scripts/ace_admin_cn/assets/css/ace-skins.min.css" />
    <style>
        .changeColor {
            background-color: #31b0d5 !important;
            color: white;
        }

        #tb_exprocesslist tr:nth-child(even) {
            background: #fafafa;
        }

        #tb_exprocesslist th {
            background: #efefef;
        }

        #tb_exprocesssteplist tr:nth-child(even) {
            background: #fafafa;
        }

        #tb_exprocesssteplist th {
            background: #efefef;
        }
    </style>

    <script src="Scripts/ace_admin_cn/assets/js/ace-extra.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/bootstrap.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/typeahead-bs2.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/jquery-ui-1.10.3.custom.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/jquery.ui.touch-punch.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/jquery.slimscroll.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/jquery.easy-pie-chart.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/jquery.sparkline.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/flot/jquery.flot.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/flot/jquery.flot.pie.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/flot/jquery.flot.resize.min.js"></script>
    <script src="Scripts/PMHelper.js"></script>
    <script>
        $(document).ready(function () {
            $().ready(function () {

                //1.初始化Table
                var oTable = new ExTableInit("");
                oTable.Init();

                //2.初始化Button的点击事件
                var oButtonInit = new ButtonInit();
                oButtonInit.Init();

                $('.widget-box a').click(function () {
                    var code = $(this).data('code');

                    //更新按键样式
                    $('.widget-box a').removeClass('btn-danger');
                    $('.widget-box a').addClass('btn-primary');

                    $(this).removeClass('btn-primary');
                    $(this).addClass('btn-danger');
                    //加载工艺列表
                    //alert(code);
                    LoadProcessList(code);
                });
            });
        });

       
  </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="navbar navbar-default" id="navbar">
            <script type="text/javascript">
                try { ace.settings.check('navbar', 'fixed') } catch (e) { }
            </script>

            <div class="navbar-container" id="navbar-container">
                <div class="navbar-header pull-left">
                    <a href="#" class="navbar-brand">
                        <small>
                            <i class="icon-leaf"></i>
                            智慧制造工艺后台管理系统
                        </small>
                    </a>
                    <!-- /.brand -->
                </div>
                <!-- /.navbar-header -->

                <div class="navbar-header pull-right" role="navigation">
                    <ul class="nav ace-nav">
                        <li class="grey">
                            <a data-toggle="dropdown" class="dropdown-toggle" href="#">
                                <i class="icon-tasks"></i>
                                <span class="badge badge-grey">4</span>
                            </a>

                            <ul class="pull-right dropdown-navbar dropdown-menu dropdown-caret dropdown-close">
                                <li class="dropdown-header">
                                    <i class="icon-ok"></i>
                                    还有4个任务完成
                                </li>

                                <li>
                                    <a href="#">
                                        <div class="clearfix">
                                            <span class="pull-left">软件更新</span>
                                            <span class="pull-right">65%</span>
                                        </div>

                                        <div class="progress progress-mini ">
                                            <div style="width: 65%" class="progress-bar "></div>
                                        </div>
                                    </a>
                                </li>

                                <li>
                                    <a href="#">
                                        <div class="clearfix">
                                            <span class="pull-left">硬件更新</span>
                                            <span class="pull-right">35%</span>
                                        </div>

                                        <div class="progress progress-mini ">
                                            <div style="width: 35%" class="progress-bar progress-bar-danger"></div>
                                        </div>
                                    </a>
                                </li>

                                <li>
                                    <a href="#">
                                        <div class="clearfix">
                                            <span class="pull-left">单元测试</span>
                                            <span class="pull-right">15%</span>
                                        </div>

                                        <div class="progress progress-mini ">
                                            <div style="width: 15%" class="progress-bar progress-bar-warning"></div>
                                        </div>
                                    </a>
                                </li>

                                <li>
                                    <a href="#">
                                        <div class="clearfix">
                                            <span class="pull-left">错误修复</span>
                                            <span class="pull-right">90%</span>
                                        </div>

                                        <div class="progress progress-mini progress-striped active">
                                            <div style="width: 90%" class="progress-bar progress-bar-success"></div>
                                        </div>
                                    </a>
                                </li>

                                <li>
                                    <a href="#">查看任务详情
										<i class="icon-arrow-right"></i>
                                    </a>
                                </li>
                            </ul>
                        </li>

                        <li class="purple">
                            <a data-toggle="dropdown" class="dropdown-toggle" href="#">
                                <i class="icon-bell-alt icon-animated-bell"></i>
                                <span class="badge badge-important">8</span>
                            </a>

                            <ul class="pull-right dropdown-navbar navbar-pink dropdown-menu dropdown-caret dropdown-close">
                                <li class="dropdown-header">
                                    <i class="icon-warning-sign"></i>
                                    8条通知
                                </li>

                                <li>
                                    <a href="#">
                                        <div class="clearfix">
                                            <span class="pull-left">
                                                <i class="btn btn-xs no-hover btn-pink icon-comment"></i>
                                                新闻评论
                                            </span>
                                            <span class="pull-right badge badge-info">+12</span>
                                        </div>
                                    </a>
                                </li>

                                <li>
                                    <a href="#">
                                        <i class="btn btn-xs btn-primary icon-user"></i>
                                        切换为编辑登录..
                                    </a>
                                </li>

                                <li>
                                    <a href="#">
                                        <div class="clearfix">
                                            <span class="pull-left">
                                                <i class="btn btn-xs no-hover btn-success icon-shopping-cart"></i>
                                                新订单
                                            </span>
                                            <span class="pull-right badge badge-success">+8</span>
                                        </div>
                                    </a>
                                </li>

                                <li>
                                    <a href="#">
                                        <div class="clearfix">
                                            <span class="pull-left">
                                                <i class="btn btn-xs no-hover btn-info icon-twitter"></i>
                                                粉丝
                                            </span>
                                            <span class="pull-right badge badge-info">+11</span>
                                        </div>
                                    </a>
                                </li>

                                <li>
                                    <a href="#">查看所有通知
										<i class="icon-arrow-right"></i>
                                    </a>
                                </li>
                            </ul>
                        </li>

                        <li class="green">
                            <a data-toggle="dropdown" class="dropdown-toggle" href="#">
                                <i class="icon-envelope icon-animated-vertical"></i>
                                <span class="badge badge-success">5</span>
                            </a>

                            <ul class="pull-right dropdown-navbar dropdown-menu dropdown-caret dropdown-close">
                                <li class="dropdown-header">
                                    <i class="icon-envelope-alt"></i>
                                    5条消息
                                </li>

                                <li>
                                    <a href="#">
                                        <img src="Scripts/ace_admin_cn/assets/avatars/avatar.png" class="msg-photo" alt="Alex's Avatar" />
                                        <span class="msg-body">
                                            <span class="msg-title">
                                                <span class="blue">Alex:</span>
                                                不知道写啥 ...
                                            </span>

                                            <span class="msg-time">
                                                <i class="icon-time"></i>
                                                <span>1分钟以前</span>
                                            </span>
                                        </span>
                                    </a>
                                </li>

                                <li>
                                    <a href="#">
                                        <img src="Scripts/ace_admin_cn/assets/avatars/avatar3.png" class="msg-photo" alt="Susan's Avatar" />
                                        <span class="msg-body">
                                            <span class="msg-title">
                                                <span class="blue">Susan:</span>
                                                不知道翻译...
                                            </span>

                                            <span class="msg-time">
                                                <i class="icon-time"></i>
                                                <span>20分钟以前</span>
                                            </span>
                                        </span>
                                    </a>
                                </li>

                                <li>
                                    <a href="#">
                                        <img src="Scripts/ace_admin_cn/assets/avatars/avatar4.png" class="msg-photo" alt="Bob's Avatar" />
                                        <span class="msg-body">
                                            <span class="msg-title">
                                                <span class="blue">Bob:</span>
                                                到底是不是英文 ...
                                            </span>

                                            <span class="msg-time">
                                                <i class="icon-time"></i>
                                                <span>下午3:15</span>
                                            </span>
                                        </span>
                                    </a>
                                </li>

                                <li>
                                    <a href="inbox.html">查看所有消息
										<i class="icon-arrow-right"></i>
                                    </a>
                                </li>
                            </ul>
                        </li>

                        <li class="light-blue">
                            <a data-toggle="dropdown" href="#" class="dropdown-toggle">
                                <img class="nav-user-photo" src="Scripts/ace_admin_cn/assets/avatars/user.jpg" alt="Jason's Photo" />
                                <span class="user-info">
                                    <small>欢迎光临,</small>
                                    Jason
                                </span>

                                <i class="icon-caret-down"></i>
                            </a>

                            <ul class="user-menu pull-right dropdown-menu dropdown-yellow dropdown-caret dropdown-close">
                                <li>
                                    <a href="#">
                                        <i class="icon-cog"></i>
                                        设置
                                    </a>
                                </li>

                                <li>
                                    <a href="#">
                                        <i class="icon-user"></i>
                                        个人资料
                                    </a>
                                </li>

                                <li class="divider"></li>

                                <li>
                                    <a href="#">
                                        <i class="icon-off"></i>
                                        退出
                                    </a>
                                </li>
                            </ul>
                        </li>
                    </ul>
                    <!-- /.ace-nav -->
                </div>
                <!-- /.navbar-header -->
            </div>
            <!-- /.container -->
        </div>

        <div class="main-container" id="main-container">
            <script type="text/javascript">
                try { ace.settings.check('main-container', 'fixed') } catch (e) { }
            </script>

            <div class="main-container-inner">
                <a class="menu-toggler" id="menu-toggler" href="#">
                    <span class="menu-text"></span>
                </a>

                <div class="sidebar" id="sidebar">
                    <script type="text/javascript">
                        try { ace.settings.check('sidebar', 'fixed') } catch (e) { }
                    </script>

                    <!-- #sidebar-shortcuts -->

                    <ul class="nav nav-list">
                        <li class="active">
                            <a href="ProcessManage.aspx">
                                <i class="icon-dashboard"></i>
                                <span class="menu-text">工艺设置 </span>
                            </a>
                        </li>
                        <li>
							<a href="FactoryDeviceManage.aspx">
								<i class="icon-text-width"></i>
								<span class="menu-text"> 工厂设备管理 </span>
							</a>
						</li>
                    </ul>
                    <!-- /.nav-list -->

                    <div class="sidebar-collapse" id="sidebar-collapse">
                        <i class="icon-double-angle-left" data-icon1="icon-double-angle-left" data-icon2="icon-double-angle-right"></i>
                    </div>

                    <script type="text/javascript">
                        try { ace.settings.check('sidebar', 'collapsed') } catch (e) { }
                    </script>
                </div>

                <div class="main-content">
                    <div class="breadcrumbs" id="breadcrumbs">
                        <script type="text/javascript">
                            try { ace.settings.check('breadcrumbs', 'fixed') } catch (e) { }
                        </script>

                        <ul class="breadcrumb">
                            <li>
                                <i class="icon-home home-icon"></i>
                                <a href="#">首页</a>
                            </li>
                            <li class="active">工艺设置</li>
                        </ul>
                        <!-- .breadcrumb -->
                    </div>

                    <div class="page-content">
                        <div class="page-header">
                            <h1>控制台
								<small>
                                    <i class="icon-double-angle-right"></i>
                                    查看
                                </small>
                            </h1>
                        </div>
                        <!-- /.page-header -->
                        <div class="row">
                            <div class="card-deck mb-3 text-center">
                                <%=CardList %>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col-sm-12 pricing-box">
                                <div class="widget-box">
                                    <div class="widget-header header-color-orange">
                                        <h5 class="bigger lighter">子流程表</h5>
                                    </div>
                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <div id="toolbar" class="btn-group">
                                                <button id="btn_add" type="button" class="btn btn-app btn-danger btn-xs" title="新增">
                                                    <span class="icon-plus" aria-hidden="true"></span>
                                                </button>
                                                <button id="btn_edit" type="button" class="btn btn-app btn-primary btn-xs" title="修改">
                                                    <span class="icon-edit" aria-hidden="true"></span>
                                                </button>
                                                <button id="btn_delete" type="button" class="btn btn-app btn-grey btn-xs" title="删除">
                                                    <span class="icon-trash" aria-hidden="true"></span>
                                                </button>
                                            </div>
                                            <table id="tb_exprocesslist"></table>
                                        </div>
                                    </div>
                                </div>
                            </div>


                        </div>
                        <div class="row">

                            <div class="col-sm-12 pricing-box">
                                <div class="widget-box">
                                    <div class="widget-header header-color-green">
                                        <h5 class="bigger lighter">流程步骤表</h5>
                                    </div>
                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <div id="steptoolbar" class="btn-group">
                                                <button id="btn_addStep" type="button" class="btn btn-app btn-danger btn-xs" title="新增">
                                                    <span class="icon-plus" aria-hidden="true"></span>
                                                </button>
                                                <button id="btn_editStep" type="button" class="btn btn-app btn-primary btn-xs" title="修改">
                                                    <span class="icon-edit" aria-hidden="true"></span>
                                                </button>
                                                <button id="btn_deleteStep" type="button" class="btn btn-app btn-grey btn-xs" title="删除">
                                                    <span class="icon-trash" aria-hidden="true"></span>
                                                </button>
                                            </div>
                                            <table id="tb_exprocesssteplist"></table>
                                        </div>
                                    </div>
                                </div>
                            </div>


                        </div>

                        <div class="row">

                            <div class="col-sm-12 pricing-box">
                                <div class="widget-box">
                                    <div class="widget-header header-color-green">
                                        <h5 class="bigger lighter">流程步骤参数表</h5>
                                    </div>
                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <div id="partoolbar" class="btn-group">
                                                <button id="btn_addPar" type="button" class="btn btn-app btn-danger btn-xs" title="新增">
                                                    <span class="icon-plus" aria-hidden="true"></span>
                                                </button>
                                                <button id="btn_editPar" type="button" class="btn btn-app btn-primary btn-xs" title="修改">
                                                    <span class="icon-edit" aria-hidden="true"></span>
                                                </button>
                                                <button id="btn_deletePar" type="button" class="btn btn-app btn-grey btn-xs" title="删除">
                                                    <span class="icon-trash" aria-hidden="true"></span>
                                                </button>
                                            </div>
                                            <table id="tb_exprocessstepparlist"></table>
                                        </div>
                                    </div>
                                </div>
                            </div>


                        </div>

                    </div>
                    <!-- /.page-content -->
                </div>
                <!-- /.main-content -->

                <div class="ace-settings-container" id="ace-settings-container">
                    <div class="btn btn-app btn-xs btn-warning ace-settings-btn" id="ace-settings-btn">
                        <i class="icon-cog bigger-150"></i>
                    </div>

                    <div class="ace-settings-box" id="ace-settings-box">
                        <div>
                            <div class="pull-left">
                                <select id="skin-colorpicker" class="hide">
                                    <option data-skin="default" value="#438EB9">#438EB9</option>
                                    <option data-skin="skin-1" value="#222A2D">#222A2D</option>
                                    <option data-skin="skin-2" value="#C6487E">#C6487E</option>
                                    <option data-skin="skin-3" value="#D0D0D0">#D0D0D0</option>
                                </select>
                            </div>
                            <span>&nbsp; 选择皮肤</span>
                        </div>

                        <div>
                            <input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-navbar" />
                            <label class="lbl" for="ace-settings-navbar">固定导航条</label>
                        </div>

                        <div>
                            <input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-sidebar" />
                            <label class="lbl" for="ace-settings-sidebar">固定滑动条</label>
                        </div>

                        <div>
                            <input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-breadcrumbs" />
                            <label class="lbl" for="ace-settings-breadcrumbs">固定面包屑</label>
                        </div>

                        <div>
                            <input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-rtl" />
                            <label class="lbl" for="ace-settings-rtl">切换到左边</label>
                        </div>

                        <div>
                            <input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-add-container" />
                            <label class="lbl" for="ace-settings-add-container">
                                切换窄屏
								<b></b>
                            </label>
                        </div>
                    </div>
                </div>
                <!-- /#ace-settings-container -->
            </div>
            <!-- /.main-container-inner -->

            <a href="#" id="btn-scroll-up" class="btn-scroll-up btn btn-sm btn-inverse">
                <i class="icon-double-angle-up icon-only bigger-110"></i>
            </a>
        </div>
        <!-- /.main-container -->
    </form>
</body>
</html>
