var ashxUrl = "/Ashx/FactoryHandler.ashx";

function LoadLineList(pcode) {
    $.ajax({
        type: "post",
        url: ashxUrl,
        data: {
            FactoryId: pcode,
            Action: "GetFactoryLineList"
        },
        dataType: "json",
        success: function (json) {
            $("#tb_linelist").bootstrapTable('load', json);
        }
    });

}

function LoadDeviceList(pcode) {
    $.ajax({
        type: "post",
        url: ashxUrl,
        data: {
            LineId: pcode,
            Action: "GetLineDevices"
        },
        dataType: "json",
        success: function (json) {
            $("#tb_devicelist").bootstrapTable('load', json);

            //刷新传感器列表
            LoadSensorList("");
            //LoadDeviceSelect(json);
        }
    });

}

function LoadSensorList(pcode) {
    if (pcode != "") {
        $.ajax({
            type: "post",
            url: ashxUrl,
            data: {
                DeviceId: pcode,
                Action: "GetDeviceSensors"
            },
            dataType: "json",
            success: function (json) {
                $("#tb_sensorlist").bootstrapTable('load', json);
            }
        });
    } else {
        $("#tb_sensorlist").bootstrapTable('removeAll');
    }
}


function RefreshSensorList() {
    //获取选择的设备
    var devices = $("#tb_devicelist").bootstrapTable('getSelections');//获取选中行的数据
    if (devices.length > 0) {
        var device = devices[0];
        var deviceId = device.DeviceId;
        LoadSensorList(deviceId);
    }
}

function RefreshDeviceList() {
    //获取选择的设备
    var lines = $("#tb_linelist").bootstrapTable('getSelections');//获取选中行的数据
    if (lines.length > 0) {
        var line = lines[0];
        var lineid = line.AssemblyLineId;
        LoadDeviceList(lineid);

    }
}

/**
 * 加载工厂所有设备列表
 * @param {any} pcode   工厂编号
 */
function LoadFactoryDevices(pcode) {
    $.ajax({
        type: "post",
        url: ashxUrl,
        data: {
            FactoryId: pcode,
            Action: "GetFactoryDevices"
        },
        dataType: "json",
        success: function (json) {
            LoadDeviceSelect(json);
        }
    });

}


/**
 * 加载生产线上的设备集合来获取相应的列表
 * @param {any} datas   数据集合
 */
function LoadDeviceSelect(datas) {
    var selectId = "sel-Device";
    CreateDeviceSelect(selectId, datas);

    selectId = "sel-ToDevice";
    CreateDeviceSelect(selectId, datas);
}
function CreateDeviceSelect(selectId, datas) {
    var selectObj = $("#" + selectId);
    var configs = datas;
    selectObj.find("option:not(:first)").remove();
    for (var i in configs) {
        var data = configs[i];
        var optionValue = data.DeviceId;
        var optionText = data.DeviceName;
        selectObj.append(new Option(optionText, optionValue));
    }

    selectObj.trigger("chosen:updated");
}


var LineTableInit = function (pcode) {
    var oTableInit = new Object();

    var ochildTable = new deviceTableInit("");
    ochildTable.Init();

    //初始化Table
    oTableInit.Init = function () {
        $('#tb_linelist').bootstrapTable({
            url: ashxUrl,         //请求后台的URL（*）
            method: 'get',                      //请求方式（*）
            toolbar: '#linetoolbar',                //工具按钮用哪个容器
            striped: true,                      //是否显示行间隔色
            cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: true,                   //是否显示分页（*）
            sortable: false,                     //是否启用排序
            sortOrder: "asc",                   //排序方式
            queryParams: oTableInit.queryParams,//传递参数（*）
            sidePagination: "client",           //分页方式：client客户端分页，server服务端分页（*）
            pageNumber: 1,                       //初始化加载第一页，默认第一页
            pageSize: 10,                       //每页的记录行数（*）
            pageList: [10, 25, 50, 100],        //可供选择的每页的行数（*）
            search: false,                       //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
            strictSearch: true,
            showColumns: false,                  //是否显示所有的列(列选择器)
            showRefresh: false,                  //是否显示刷新按钮
            minimumCountColumns: 2,             //最少允许的列数
            clickToSelect: true,                //是否启用点击选中行

            uniqueId: "AssemblyLineId",                     //每一行的唯一标识，一般为主键列
            showToggle: false,                    //是否显示详细视图和列表视图的切换按钮
            cardView: false,                    //是否显示详细视图
            detailView: false,                   //是否显示父子表
            singleSelect: true,                    //是否单选
            columns: [{
                checkbox: true
            }, {
                field: 'AssemblyLineId',
                title: '生产线编号'
            }, {
                field: 'ProcessId',
                title: '当前工艺流程编号'
            }, {
                field: 'AssemblyLineTitle',
                title: '生产线名称'
            }, {
                field: 'Parameter1',
                title: '说明1'
            }, {
                field: 'Parameter2',
                title: '说明2'
            }

            ],
            onClickRow: function (row, $element) {
                //$('.info').removeClass('info');
                //$($element).addClass('info');
                //alert(row);
                var lineId = row.AssemblyLineId;
                LoadDeviceList(lineId);

                //$('.info').removeClass('info');//移除class
                //$($element).addClass('info');//添加class
            }
        });
    };

    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit,   //页面大小
            offset: params.offset,  //页码
            FactoryId: pcode,
            Action: "GetFactoryLineList"
        };
        return temp;
    };
    return oTableInit;
};

var ButtonInit = function () {
    var oInit = new Object();
    var postdata = {};

    oInit.Init = function () {
        //初始化页面上面的按钮事件
    };

    return oInit;
};


var deviceTableInit = function (pcode) {
    var oTableInit = new Object();

    var oSensorTable = new sensorTableInit("");
    oSensorTable.Init();
    //初始化Table
    oTableInit.Init = function () {
        $('#tb_devicelist').bootstrapTable({
            url: ashxUrl,         //请求后台的URL（*）
            method: 'get',                      //请求方式（*）
            toolbar: '#devicetoolbar',                //工具按钮用哪个容器
            striped: true,                      //是否显示行间隔色
            cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: true,                   //是否显示分页（*）
            sortable: false,                     //是否启用排序
            sortOrder: "asc",                   //排序方式
            queryParams: oTableInit.queryParams,//传递参数（*）
            sidePagination: "client",           //分页方式：client客户端分页，server服务端分页（*）
            pageNumber: 1,                       //初始化加载第一页，默认第一页
            pageSize: 10,                       //每页的记录行数（*）
            pageList: [10, 25, 50, 100],        //可供选择的每页的行数（*）
            search: false,                       //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
            strictSearch: true,
            showColumns: false,                  //是否显示所有的列(列选择器)
            showRefresh: false,                  //是否显示刷新按钮
            minimumCountColumns: 2,             //最少允许的列数
            clickToSelect: true,                //是否启用点击选中行
            height: 500,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
            uniqueId: "DeviceId",                     //每一行的唯一标识，一般为主键列
            showToggle: false,                    //是否显示详细视图和列表视图的切换按钮
            cardView: false,                    //是否显示详细视图
            detailView: false,                   //是否显示父子表
            singleSelect: true,                    //是否单选

            columns: [{
                checkbox: true
            }, {
                field: 'DeviceId',
                title: '设备编号'
            }, {
                field: 'DeviceName',
                title: '设备名称'
            }, {
                field: 'DeviceTypeString',
                title: '设备类型'
            }, {
                field: 'ProcessDeviceId',
                title: '工艺设备编号'
            }, {
                field: 'DeviceStatus',
                title: '设备状态'
            }, {
                field: 'Parameter1',
                title: '说明1'
            }, {
                field: 'Parameter2',
                title: '说明2'
            }
            ],
            onClickRow: function (row, $element) {
                //$('.info').removeClass('info');
                //$($element).addClass('info');
                //alert(row);

                var deviceId = row.DeviceId;
                LoadSensorList(deviceId);
            }
        });
    };
    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit,   //页面大小
            offset: params.offset,  //页码
            LineId: pcode,
            Action: "GetLineDevices"
        };
        return temp;
    };
    return oTableInit;
};

//初始化传感器列表
var sensorTableInit = function (pcode) {
    var oTableInit = new Object();

    //var oParTable = new parTableInit("");
    //oParTable.Init();
    //初始化Table
    oTableInit.Init = function () {
        $('#tb_sensorlist').bootstrapTable({
            url: ashxUrl,         //请求后台的URL（*）
            method: 'get',                      //请求方式（*）
            toolbar: '#sonsertoolbar',                //工具按钮用哪个容器
            striped: true,                      //是否显示行间隔色
            cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: true,                   //是否显示分页（*）
            sortable: false,                     //是否启用排序
            sortOrder: "asc",                   //排序方式
            queryParams: oTableInit.queryParams,//传递参数（*）
            sidePagination: "client",           //分页方式：client客户端分页，server服务端分页（*）
            pageNumber: 1,                       //初始化加载第一页，默认第一页
            pageSize: 10,                       //每页的记录行数（*）
            pageList: [10, 25, 50, 100],        //可供选择的每页的行数（*）
            search: false,                       //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
            strictSearch: true,
            showColumns: false,                  //是否显示所有的列(列选择器)
            showRefresh: false,                  //是否显示刷新按钮
            minimumCountColumns: 2,             //最少允许的列数
            clickToSelect: true,                //是否启用点击选中行
            height: 350,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
            uniqueId: "SensorId",                     //每一行的唯一标识，一般为主键列
            showToggle: false,                    //是否显示详细视图和列表视图的切换按钮
            cardView: false,                    //是否显示详细视图
            detailView: false,                   //是否显示父子表
            singleSelect: true,                    //是否单选

            columns: [{
                checkbox: true
            }, {
                field: 'SensorId',
                title: '传感器编号'
            }, {
                field: 'SensorName',
                title: '传感器名称'
            }, {
                field: 'SensorStatus',
                title: '状态'
            }, {
                field: 'ToDeviceName',
                title: '连接设备'
            }, {
                field: 'PLC_Id',
                title: 'PLC机柜'
            }, {
                field: 'PLC_Address',
                title: 'PLC地址'
            }, {
                field: 'MinEU',
                title: '最小参数'
            }, {
                field: 'MaxEu',
                title: '最大参数'
            }, {
                field: 'Units',
                title: '单位'
            }, {
                field: 'ParType',
                title: '参数类型'
            }, {
                field: 'SComment',
                title: '说明'
            }
            ],
            onClickRow: function (row, $element) {
                //$('.info').removeClass('info');
                //$($element).addClass('info');
                //alert(row);

                //var deviceId = row.DeviceId;
                //LoadSensorList(deviceId);
            }
        });
    };
    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit,   //页面大小
            offset: params.offset,  //页码
            LineId: pcode,
            Action: "GetDeviceSensors"
        };
        return temp;
    };
    return oTableInit;
};