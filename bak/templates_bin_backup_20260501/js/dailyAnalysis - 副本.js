var DailyAnalysis = (function() {
    var queryConfig = [];
    var sortColumn = '';
    var sortDirection = 'DESC';
    var chartInstance = null;
    var isDataLoaded = false;
    var initCallbacks = [];

    function preloadData() {
        loadAllOptions(function() {
            loadQueryConfig(function() {
                isDataLoaded = true;
                initCallbacks.forEach(function(cb) { cb(); });
                initCallbacks = [];
            });
        });
    }

    if (typeof window !== 'undefined') {
        window.addEventListener('DOMContentLoaded', preloadData);
    }

    var ErrorHandler = {
        show: function(message, type) {
            type = type || 'error';
            var alertClass = type === 'success' ? 'alert-success' : 
                           type === 'warning' ? 'alert-warning' : 'alert-danger';
            
            var alertHtml = '<div class="alert ' + alertClass + ' alert-dismissible fade show" role="alert">' +
                           message +
                           '<button type="button" class="btn-close" data-bs-dismiss="alert"></button></div>';
            
            $('#panelContent').prepend(alertHtml);
            setTimeout(function() {
                $('.alert').fadeOut();
            }, 5000);
        },
        
        handleAjaxError: function(xhr, status, error) {
            var message = '请求失败';
            if (xhr.status === 404) {
                message = '服务接口不存在';
            } else if (xhr.status === 500) {
                message = '服务器内部错误';
            } else if (error) {
                message = error;
            }
            ErrorHandler.show(message, 'error');
            console.error('AJAX Error:', status, error);
        },
        
        validateResponse: function(response) {
            try {
                var resp = typeof response === 'string' ? JSON.parse(response) : response;
                if (!resp.success) {
                    ErrorHandler.show(resp.error || '操作失败', 'warning');
                    return null;
                }
                return resp.data;
            } catch (e) {
                ErrorHandler.show('数据解析失败', 'error');
                console.error('Parse Error:', e);
                return null;
            }
        }
    };

    function loadQueryConfig(callback) {
        $.ajax({
            url: '/get-query-config',
            type: 'GET',
            success: function(response) {
                var data = ErrorHandler.validateResponse(response);
                if (data) {
                    queryConfig = data;
                    if (callback) callback();
                }
            },
            error: ErrorHandler.handleAjaxError
        });
    }

    var allOptionsCache = null;

    function loadAllOptions(callback) {
        if (allOptionsCache) {
            if (callback) callback();
            return;
        }
        
        $.ajax({
            url: '/get-all-options',
            type: 'GET',
            cache: false,
            success: function(response) {
                try {
                    var resp = typeof response === 'string' ? JSON.parse(response) : response;
                    if (resp.success) {
                        allOptionsCache = resp.data;
                        if (callback) callback();
                    } else {
                        ErrorHandler.show(resp.error || '加载选项数据失败', 'error');
                    }
                } catch (e) {
                    ErrorHandler.show('数据解析失败', 'error');
                    console.error('Parse Error:', e);
                }
            },
            error: function(xhr, status, error) {
                ErrorHandler.show('加载选项数据失败: ' + (error || '网络错误'), 'error');
                console.error('AJAX Error:', status, error);
            }
        });
    }

    function loadFieldOptions(fieldName, parentValue, callback) {
        var select = $('#' + fieldName);
        var config = queryConfig.find(function(c) { return c.fieldName === fieldName; });
        
        if (!allOptionsCache) {
            select.empty();
            if (config && config.isMultiple) {
                select.append('<option value="">加载中...</option>');
            } else {
                select.append('<option value="">' + (config ? config.placeholder : '请选择') + '</option>');
                select.append('<option value="" disabled>加载中...</option>');
            }
            setTimeout(function() {
                loadFieldOptions(fieldName, parentValue, callback);
            }, 100);
            return;
        }
        
        var data = [];
        switch(fieldName) {
            case 'system':
                data = allOptionsCache.systems || [];
                break;
            case 'reporter':
                data = allOptionsCache.reporters || [];
                break;
            case 'reviewer':
                data = allOptionsCache.reviewers || [];
                break;
            case 'technician':
                data = allOptionsCache.technicians || [];
                break;
            case 'department':
                data = allOptionsCache.departments || [];
                break;
            case 'category':
                data = allOptionsCache.categories || [];
                break;
            case 'patientType':
                data = allOptionsCache.patientTypes || [];
                break;
            case 'resultStatus':
                data = allOptionsCache.resultStatus || [];
                break;
        }
        
        select.empty();
        if (config && config.isMultiple) {
            data.forEach(function(item) {
                select.append('<option value="' + htmlEncode(item.code) + '">' + htmlEncode(item.name) + '</option>');
            });
        } else {
            select.append('<option value="">' + (config ? config.placeholder : '请选择') + '</option>');
            data.forEach(function(item) {
                select.append('<option value="' + htmlEncode(item.code) + '">' + htmlEncode(item.name) + '</option>');
            });
        }
        
        if (callback) callback();
    }

    function htmlEncode(str) {
        if (!str) return '';
        return str.replace(/&/g, '&amp;')
                  .replace(/</g, '&lt;')
                  .replace(/>/g, '&gt;')
                  .replace(/"/g, '&quot;');
    }

    function onParentFieldChanged(fieldName) {
        var config = queryConfig.find(function(c) { return c.fieldName === fieldName; });
        if (!config) return;
        
        var parentValue = $('#' + fieldName).val();
        
        queryConfig.forEach(function(childConfig) {
            if (childConfig.parentField === fieldName) {
                loadFieldOptions(childConfig.fieldName, parentValue);
            }
        });
    }

    function generateDynamicFilters() {
        var container = $('#dynamicFilters');
        container.empty();
        
        var visibleFields = queryConfig.filter(function(c) { return c.isVisible; });
        
        visibleFields.forEach(function(config) {
            var displayName = config.displayName;
            var placeholder = config.placeholder;
            
            if (config.fieldName === 'resultStatus') {
                displayName = '阴阳性';
                placeholder = '请选择阴阳性';
            }
            if (config.fieldName === 'patientType') {
                displayName = '病人类型';
                placeholder = '请选择病人类型';
            }
            
            var fieldHtml = '<div class="da-filter-item">';
            fieldHtml += '<label>' + htmlEncode(displayName) + '</label>';
            
            if (config.isMultiple) {
                fieldHtml += '<select id="' + config.fieldName + '" multiple>';
                fieldHtml += '<option value="">加载中...</option>';
                fieldHtml += '</select>';
            } else {
                fieldHtml += '<select id="' + config.fieldName + '" onchange="DailyAnalysis.onParentFieldChanged(\'' + config.fieldName + '\')">';
                fieldHtml += '<option value="">' + placeholder + '</option>';
                fieldHtml += '</select>';
            }
            
            fieldHtml += '</div>';
            container.append(fieldHtml);
        });
    }

    function loadAllFieldOptions() {
        queryConfig.forEach(function(config) {
            if (config.isVisible) {
                loadFieldOptions(config.fieldName, '');
            }
        });
    }

    function runDailyAnalysis() {
        var startDate = $('#startDate').val();
        var endDate = $('#endDate').val();
        
        if (!startDate || !endDate) {
            ErrorHandler.show('请选择日期范围', 'warning');
            return;
        }
        
        var data = {startDate: startDate, endDate: endDate};
        
        queryConfig.forEach(function(config) {
            if (config.isVisible) {
                var value = $('#' + config.fieldName).val();
                if (config.isMultiple && Array.isArray(value)) {
                    data[config.fieldName] = value.join(',');
                } else {
                    data[config.fieldName] = value || '';
                }
            }
        });
        
        data.sortBy = sortColumn || '任务数量';
        data.sortOrder = sortDirection;
        
        $('#analysisResult').html('<div class="da-loading"><span class="spinner-border spinner-border-sm"></span><div style="margin-top:12px;font-size:0.95rem;">数据加载中...</div></div>');
        hideChart();
        
        $.ajax({
            url: '/daily-analysis',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            success: function(response) {
                var data = ErrorHandler.validateResponse(response);
                if (data) {
                    displayAnalysisResult(data);
                    updateStatistics(data);
                    renderChart(data);
                } else {
                    resetStatistics();
                }
            },
            error: function(xhr, status, error) {
                ErrorHandler.handleAjaxError(xhr, status, error);
                $('#analysisResult').html('<div class="alert alert-danger">加载失败</div>');
                resetStatistics();
            }
        });
    }

    function displayAnalysisResult(data) {
        if (!data || data.length === 0) {
            $('#analysisResult').html('<div class="da-empty-state">📭 没有查询到数据</div>');
            return;
        }
        
        var columns = Object.keys(data[0]);
        var tableHtml = '<div class="da-table-wrapper" style="max-height:520px;overflow-y:auto;">';
        tableHtml += '<table class="table table-striped table-bordered" style="margin-bottom:0;">';
        tableHtml += '<thead class="table-dark sticky-top"><tr>';
        
        for (var i = 0; i < columns.length; i++) {
            var col = columns[i];
            var sortIcon = '';
            if (sortColumn === col) {
                sortIcon = sortDirection === 'ASC' ? ' ▲' : ' ▼';
            }
            tableHtml += '<th style="cursor:pointer;white-space:nowrap;" onclick="DailyAnalysis.sortByColumn(\'' + col + '\')">' + htmlEncode(col) + sortIcon + '</th>';
        }
        
        tableHtml += '</tr></thead><tbody>';
        
        for (var i = 0; i < data.length; i++) {
            tableHtml += '<tr>';
            for (var j = 0; j < columns.length; j++) {
                var value = data[i][columns[j]];
                if (columns[j] === '阳性率') {
                    var rateStr = value ? value.toFixed(2) + '%' : '-';
                    var rateClass = value >= 50 ? 'style="color:#dc2626;font-weight:600;"' : 'style="color:#16a34a;font-weight:600;"';
                    tableHtml += '<td ' + rateClass + '>' + rateStr + '</td>';
                } else {
                    tableHtml += '<td>' + htmlEncode(value !== null && value !== undefined ? value : '') + '</td>';
                }
            }
            tableHtml += '</tr>';
        }
        
        tableHtml += '</tbody></table></div>';
        tableHtml += '<div style="margin-top:12px;color:#64748b;font-size:0.85rem;">共 ' + data.length + ' 条记录（点击列头排序）</div>';
        $('#analysisResult').html(tableHtml);
    }

    function sortByColumn(column) {
        if (sortColumn === column) {
            sortDirection = sortDirection === 'ASC' ? 'DESC' : 'ASC';
        } else {
            sortColumn = column;
            sortDirection = 'DESC';
        }
        runDailyAnalysis();
    }

    function clearAllFilters() {
        queryConfig.forEach(function(config) {
            if (config.isVisible) {
                $('#' + config.fieldName).val('');
            }
        });
        sortColumn = '';
        sortDirection = 'DESC';
        resetStatistics();
        hideChart();
            $('#analysisResult').html('<div class="da-empty-state">📭 请选择日期范围并点击查询</div>');
    }

    function runAnalysis(type) {
        var startDate = $('#startDate').val();
        var endDate = $('#endDate').val();
        var system = $('#system').val() || '';
        
        if (!startDate || !endDate) {
            ErrorHandler.show('请选择日期范围', 'warning');
            return;
        }
        
        var url = '/daily-analysis';
        var data = {startDate: startDate, endDate: endDate, system: system};
        
        if (type === 'department') {
            url = '/department-statistics';
        } else if (type === 'doctor') {
            url = '/doctor-statistics';
            data.doctorType = 'reporter';
        } else if (type === 'category') {
            url = '/category-statistics';
        }
        
        $('#analysisResult').html('<div class="da-loading"><div class="spinner-border" role="status" style="width:2.5rem;height:2.5rem;border-width:3px;color:#1a73e8;"></div><div style="margin-top:12px;font-size:0.95rem;color:#64748b;">数据加载中...</div></div>');
        
        $.ajax({
            url: url,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            success: function(response) {
                var data = ErrorHandler.validateResponse(response);
                if (data) {
                    displayAnalysisResult(data);
                    updateStatistics(data);
                    renderChart(data);
                }
            },
            error: ErrorHandler.handleAjaxError
        });
    }

    function updateStatistics(data) {
        if (!data || data.length === 0) {
            resetStatistics();
            return;
        }
        
        var total = 0, positive = 0, negative = 0;
        
        data.forEach(function(row) {
            var count = parseInt(row['任务数量']) || 0;
            total += count;
            positive += parseInt(row['阳性数量']) || 0;
            negative += parseInt(row['阴性数量']) || 0;
        });
        
        var rate = total > 0 ? (positive / total * 100) : 0;
        
        $('#positiveRate').text(rate.toFixed(2) + '%');
        $('#totalCount').text(total);
        $('#positiveCount').text(positive);
        $('#negativeCount').text(negative);
    }

    function resetStatistics() {
        $('#positiveRate').text('-');
        $('#totalCount').text('-');
        $('#positiveCount').text('-');
        $('#negativeCount').text('-');
    }

    function hideChart() {
        if (chartInstance) {
            chartInstance.dispose();
            chartInstance = null;
        }
        $('#chartSection').hide();
    }

    function renderChart(data) {
        if (!data || data.length === 0) {
            hideChart();
            return;
        }
        
        $('#chartSection').show();
        
        var container = document.getElementById('chartContainer');
        if (!container) return;
        
        if (chartInstance) {
            chartInstance.dispose();
        }
        
        chartInstance = echarts.init(container);
        
        var categoryData = [];
        var positiveData = [];
        var negativeData = [];
        var rateData = [];
        
        var topData = data.slice(0, 12);
        topData.forEach(function(row) {
            var name = row['报告医生'] || row['执行科室'] || row['检查类型'] || '未知';
            categoryData.push(name.length > 6 ? name.substring(0, 6) + '..' : name);
            positiveData.push(row['阳性数量'] || 0);
            negativeData.push(row['阴性数量'] || 0);
            rateData.push(row['阳性率'] || 0);
        });
        
        var option = {
            title: {
                text: '📊 检查结果统计',
                left: 'center',
                top: 10,
                textStyle: {
                    color: '#1e293b',
                    fontSize: 16,
                    fontWeight: 700,
                    fontFamily: 'Microsoft YaHei, PingFang SC, sans-serif'
                }
            },
            tooltip: {
                trigger: 'axis',
                axisPointer: { type: 'shadow' },
                backgroundColor: 'rgba(255,255,255,0.96)',
                borderColor: '#e2e8f0',
                borderWidth: 1,
                textStyle: { color: '#334155', fontSize: 13 },
                formatter: function(params) {
                    var result = '<div style="font-weight:600;margin-bottom:8px;">' + params[0].name + '</div>';
                    params.forEach(function(p) {
                        result += '<div style="display:flex;align-items:center;gap:6px;margin:4px 0;">' +
                                  '<span style="display:inline-block;width:10px;height:10px;border-radius:50%;background:' + p.color + ';"></span>' +
                                  p.seriesName + '：<strong>' + p.value + '</strong></div>';
                    });
                    return result;
                }
            },
            legend: {
                data: ['阳性', '阴性', '阳性率'],
                bottom: 10,
                textStyle: { color: '#64748b', fontSize: 12 }
            },
            grid: {
                left: '3%',
                right: '4%',
                bottom: '18%',
                top: '18%',
                containLabel: true
            },
            xAxis: {
                type: 'category',
                data: categoryData,
                axisLabel: {
                    interval: 0,
                    rotate: 30,
                    fontSize: 11,
                    color: '#64748b'
                },
                axisLine: { lineStyle: { color: '#e2e8f0' } }
            },
            yAxis: [
                {
                    type: 'value',
                    name: '数量',
                    nameTextStyle: { color: '#64748b', fontSize: 12 },
                    axisLabel: { color: '#64748b', fontSize: 11 },
                    axisLine: { show: false },
                    splitLine: { lineStyle: { color: '#f1f5f9', type: 'dashed' } }
                },
                {
                    type: 'value',
                    name: '阳性率(%)',
                    nameTextStyle: { color: '#64748b', fontSize: 12 },
                    axisLabel: { color: '#64748b', fontSize: 11, formatter: '{value}%' },
                    axisLine: { show: false },
                    splitLine: { show: false },
                    min: 0,
                    max: 100
                }
            ],
            series: [
                {
                    name: '阳性',
                    type: 'bar',
                    barWidth: '28%',
                    itemStyle: {
                        color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
                            { offset: 0, color: '#f87171' },
                            { offset: 1, color: '#dc2626' }
                        ]),
                        borderRadius: [4, 4, 0, 0]
                    },
                    data: positiveData
                },
                {
                    name: '阴性',
                    type: 'bar',
                    barWidth: '28%',
                    itemStyle: {
                        color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
                            { offset: 0, color: '#4ade80' },
                            { offset: 1, color: '#16a34a' }
                        ]),
                        borderRadius: [4, 4, 0, 0]
                    },
                    data: negativeData
                },
                {
                    name: '阳性率',
                    type: 'line',
                    yAxisIndex: 1,
                    smooth: true,
                    symbol: 'circle',
                    symbolSize: 8,
                    lineStyle: { color: '#f59e0b', width: 3 },
                    itemStyle: {
                        color: '#f59e0b',
                        borderColor: '#fff',
                        borderWidth: 2
                    },
                    data: rateData
                }
            ]
        };
        
        chartInstance.setOption(option);
        
        window.addEventListener('resize', function() {
            if (chartInstance) {
                chartInstance.resize();
            }
        });
    }

    function setDateRange(range) {
        var today = new Date();
        var startDate, endDate;
        
        switch (range) {
            case 'today':
                startDate = endDate = today;
                break;
            case 'yesterday':
                startDate = endDate = new Date(today.getTime() - 86400000);
                break;
            case 'thisweek':
                var day = today.getDay();
                var diff = today.getDate() - day + (day === 0 ? -6 : 1);
                startDate = new Date(today.setDate(diff));
                endDate = new Date();
                break;
            case 'lastweek':
                var lastWeekStart = new Date(today.getTime() - 7 * 86400000);
                var lastDay = lastWeekStart.getDay();
                var lastDiff = lastWeekStart.getDate() - lastDay + (lastDay === 0 ? -6 : 1);
                startDate = new Date(lastWeekStart.setDate(lastDiff));
                endDate = new Date(startDate.getTime() + 6 * 86400000);
                break;
            case 'thismonth':
                startDate = new Date(today.getFullYear(), today.getMonth(), 1);
                endDate = new Date();
                break;
            case 'lastmonth':
                startDate = new Date(today.getFullYear(), today.getMonth() - 1, 1);
                endDate = new Date(today.getFullYear(), today.getMonth(), 0);
                break;
        }
        
        $('#startDate').val(startDate.toISOString().split('T')[0]);
        $('#endDate').val(endDate.toISOString().split('T')[0]);
    }

    function init() {
        history.pushState({page: 'dailyAnalysis'}, '检查每日分析', '#daily-analysis');
        $('#mainMenu').hide();
        $('#configPanel').show();
        $('#panelTitle').text('检查每日分析');

        // 内嵌样式 - 现代高端设计
        var pageStyle = `
        <style>
        .da-page {
            max-width: 1400px;
            margin: 0 auto;
            animation: fadeIn 0.4s ease-out;
        }
        @keyframes fadeIn {
            from { opacity: 0; transform: translateY(10px); }
            to { opacity: 1; transform: translateY(0); }
        }
        /* 查询区域卡片 */
        .da-query-card {
            background: linear-gradient(145deg, #ffffff, #f8fafc);
            border-radius: 24px;
            padding: 32px;
            margin-bottom: 28px;
            box-shadow: 0 12px 40px rgba(0,0,0,0.08), inset 0 1px 0 rgba(255,255,255,0.8);
            border: 1px solid rgba(0,0,0,0.05);
        }
        .da-query-card label {
            color: #1e293b !important;
            font-weight: 600;
            font-size: 0.95rem;
            margin-bottom: 10px;
        }
        /* 统计卡片 */
        .da-stat-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(240px, 1fr));
            gap: 24px;
            margin-bottom: 28px;
        }
        .da-stat-card {
            background: linear-gradient(145deg, #ffffff, #fafafa);
            border-radius: 20px;
            padding: 28px;
            box-shadow: 0 6px 24px rgba(0,0,0,0.06), inset 0 1px 0 rgba(255,255,255,0.6);
            border: 1px solid rgba(0,0,0,0.04);
            transition: all 0.4s cubic-bezier(0.4, 0, 0.2, 1);
            position: relative;
            overflow: hidden;
        }
        .da-stat-card::before {
            content: '';
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            height: 4px;
            border-radius: 20px 20px 0 0;
        }
        .da-stat-card:nth-child(1)::before { background: linear-gradient(90deg, #3b82f6, #60a5fa, #93c5fd); }
        .da-stat-card:nth-child(2)::before { background: linear-gradient(90deg, #8b5cf6, #a78bfa, #c4b5fd); }
        .da-stat-card:nth-child(3)::before { background: linear-gradient(90deg, #ef4444, #f87171, #fca5a5); }
        .da-stat-card:nth-child(4)::before { background: linear-gradient(90deg, #22c55e, #4ade80, #86efac); }
        .da-stat-card:hover {
            transform: translateY(-6px) scale(1.02);
            box-shadow: 0 16px 50px rgba(0,0,0,0.12);
        }
        .da-stat-label {
            font-size: 0.95rem;
            color: #64748b;
            font-weight: 500;
            margin-bottom: 12px;
            display: flex;
            align-items: center;
            gap: 8px;
        }
        .da-stat-value {
            font-size: 2.5rem;
            font-weight: 800;
            line-height: 1.1;
            letter-spacing: -1px;
        }
        .da-stat-card:nth-child(1) .da-stat-value { color: #1d4ed8; text-shadow: 0 2px 8px rgba(26,115,232,0.15); }
        .da-stat-card:nth-child(2) .da-stat-value { color: #6d28d9; text-shadow: 0 2px 8px rgba(109,40,217,0.15); }
        .da-stat-card:nth-child(3) .da-stat-value { color: #dc2626; text-shadow: 0 2px 8px rgba(220,38,38,0.15); }
        .da-stat-card:nth-child(4) .da-stat-value { color: #16a34a; text-shadow: 0 2px 8px rgba(22,163,74,0.15); }
        /* 结果区域卡片 */
        .da-result-card {
            background: linear-gradient(145deg, #ffffff, #fafafa);
            border-radius: 24px;
            padding: 32px;
            box-shadow: 0 6px 24px rgba(0,0,0,0.06), inset 0 1px 0 rgba(255,255,255,0.6);
            border: 1px solid rgba(0,0,0,0.04);
            margin-bottom: 28px;
        }
        .da-result-card h5 {
            font-weight: 700;
            color: #1e293b;
            font-size: 1.25rem;
            margin-bottom: 24px;
            display: flex;
            align-items: center;
            gap: 10px;
        }
        /* 图表区域卡片 */
        .da-chart-card {
            background: linear-gradient(145deg, #ffffff, #fafafa);
            border-radius: 24px;
            padding: 32px;
            box-shadow: 0 6px 24px rgba(0,0,0,0.06), inset 0 1px 0 rgba(255,255,255,0.6);
            border: 1px solid rgba(0,0,0,0.04);
            margin-bottom: 28px;
        }
        .da-chart-card h5 {
            font-weight: 700;
            color: #1e293b;
            font-size: 1.25rem;
            margin-bottom: 24px;
            display: flex;
            align-items: center;
            gap: 10px;
        }
        /* 按钮优化 */
        .da-btn-primary {
            background: linear-gradient(135deg, #1a73e8 0%, #1557b0 100%);      
            border: none;
            border-radius: 10px;
            padding: 0 20px;
            height: 38px;
            font-weight: 600;
            font-size: 0.85rem;
            color: white;
            cursor: pointer;
            transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
            box-shadow: 0 4px 16px rgba(26,115,232,0.3);
            display: inline-flex;
            align-items: center;
            gap: 6px;
        }
        .da-btn-primary:hover {
            transform: translateY(-3px);
            box-shadow: 0 10px 30px rgba(26,115,232,0.45);
        }
        .da-btn-primary:active {
            transform: translateY(-1px);
        }
        .da-btn-secondary {
            background: rgba(100,116,139,0.08);
            border: 2px solid #e2e8f0;
            border-radius: 10px;
            padding: 0 20px;
            height: 38px;
            font-weight: 600;
            font-size: 0.85rem;
            color: #64748b;
            cursor: pointer;
            transition: all 0.3s;
            display: inline-flex;
            align-items: center;
            gap: 6px;
        }
        .da-btn-secondary:hover {
            background: rgba(100,116,139,0.15);
            border-color: rgba(100,116,139,0.3);
            color: #475569;
        }
        .da-btn-outline {
            background: white;
            border: 2px solid #e2e8f0;
            border-radius: 10px;
            padding: 0 14px;
            height: 38px;
            font-weight: 600;
            font-size: 0.85rem;
            color: #475569;
            cursor: pointer;
            transition: all 0.3s;
            display: inline-flex;
            align-items: center;
            gap: 6px;
        }
        .da-btn-outline:hover {
            background: rgba(26,115,232,0.06);
            border-color: rgba(26,115,232,0.4);
            color: #1a73e8;
        }
        /* 下拉菜单优化 */
        .da-dropdown-menu {
            border-radius: 16px;
            border: 1px solid rgba(0,0,0,0.08);
            box-shadow: 0 12px 48px rgba(0,0,0,0.15);
            padding: 8px;
            min-width: 170px;
            background: white;
            animation: slideDown 0.2s ease-out;
        }
        @keyframes slideDown {
            from { opacity: 0; transform: translateY(-8px); }
            to { opacity: 1; transform: translateY(0); }
        }
        .da-dropdown-menu li {
            margin: 2px 0;
        }
        .da-dropdown-menu a {
            display: block;
            border-radius: 10px;
            padding: 10px 16px;
            font-size: 0.95rem;
            color: #334155;
            transition: all 0.2s;
            text-decoration: none;
        }
        .da-dropdown-menu a:hover {
            background: linear-gradient(135deg, rgba(26,115,232,0.08), rgba(26,115,232,0.04));
            color: #1a73e8;
            padding-left: 20px;
        }
        /* 筛选条件区域 */
        .da-filter-section {
            margin-top: 24px;
            padding-top: 24px;
            border-top: 1px solid rgba(0,0,0,0.06);
            display: flex;
            flex-wrap: wrap;
            gap: 16px;
            align-items: flex-end;
        }
        .da-filter-item {
            display: flex !important;
            flex-direction: column !important;
            width: 150px;
            flex-shrink: 0;
            align-items: flex-start !important;
        }
        .da-filter-item label {
            color: #334155;
            font-weight: 600;
            font-size: 0.85rem;
            margin-bottom: 6px;
        }
        .da-filter-item select,
        .da-filter-item input,
        .da-filter-input,
        .da-filter-item select.form-select,
        .da-filter-item input.form-control {
            border-radius: 10px !important;
            border: 2px solid #e2e8f0 !important;
            padding: 8px 12px !important;
            font-size: 0.85rem !important;
            transition: all 0.3s;
            background: white !important;
            height: 38px !important;
            cursor: pointer;
            width: 140px !important;
            min-width: 140px !important;
            max-width: 140px !important;
            box-sizing: border-box !important;
            flex-shrink: 0;
        }
        .da-filter-item select[multiple] {
            padding: 6px;
            height: 62px;
        }
        .da-filter-item select:focus,
        .da-filter-item input:focus {
            border-color: #1a73e8;
            box-shadow: 0 0 0 4px rgba(26,115,232,0.12);
            outline: none;
        }
        /* 子按钮组 */
        .da-sub-btn-group {
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
        }
        .da-sub-btn-group .da-sub-btn {
            background: linear-gradient(145deg, #f8fafc, #f1f5f9);
            border: 1px solid #e2e8f0;
            border-radius: 12px;
            padding: 10px 22px;
            font-size: 0.9rem;
            font-weight: 600;
            color: #475569;
            cursor: pointer;
            transition: all 0.3s;
            display: inline-flex;
            align-items: center;
            gap: 6px;
        }
        .da-sub-btn-group .da-sub-btn:hover {
            background: linear-gradient(135deg, rgba(26,115,232,0.1), rgba(26,115,232,0.05));
            border-color: rgba(26,115,232,0.3);
            color: #1a73e8;
            transform: translateY(-2px);
            box-shadow: 0 4px 16px rgba(26,115,232,0.15);
        }
        /* 表格容器 */
        .da-table-wrapper {
            border-radius: 16px;
            overflow: hidden;
            border: 1px solid rgba(0,0,0,0.06);
            box-shadow: 0 4px 20px rgba(0,0,0,0.04);
        }
        /* 加载状态 */
        .da-loading {
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            padding: 40px;
            color: #64748b;
        }
        /* 空状态 */
        .da-empty-state {
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            padding: 48px;
            color: #94a3b8;
            font-size: 1.1rem;
            text-align: center;
        }
        </style>`;

        var html = '<div class="da-page">';
        html += pageStyle;

        // ===== 顶部查询区域 =====
        html += '<div class="da-query-card">';

        // 所有内容放在同一行
        html += '<div style="display:flex;flex-wrap:wrap;align-items:flex-end;gap:12px;">';

        // 日期范围
        html += '<div class="da-filter-item" style="width: 280px;">';
        html += '<label style="font-size:0.85rem;color:#64748b;font-weight:500;">日期范围</label>';
        html += '<div style="display:flex;align-items:center;gap:8px;">';
        html += '<input type="date" id="startDate" class="da-filter-input">';
        html += '<span style="color:#94a3b8;font-size:0.85rem;">至</span>';
        html += '<input type="date" id="endDate" class="da-filter-input">';
        html += '</div>';
        html += '</div>';

        // 快捷选择
        html += '<div style="position:relative;">';
        html += '<button type="button" class="da-btn-outline" id="daDateDropdownBtn" style="height:38px;padding:0 14px;font-size:0.85rem;border-radius:10px;">⚡ 快捷选择 ▼</button>';
        html += '<ul class="da-dropdown-menu" id="daDateDropdown" style="display:none;position:absolute;top:100%;left:0;z-index:1000;background:white;list-style:none;margin:4px 0 0 0;">';
        html += '<li><a href="#" onclick="DailyAnalysis.setDateRange(\'today\');$(\'#daDateDropdown\').hide();return false;" style="display:block;text-decoration:none;">📅 今天</a></li>';
        html += '<li><a href="#" onclick="DailyAnalysis.setDateRange(\'yesterday\');$(\'#daDateDropdown\').hide();return false;" style="display:block;text-decoration:none;">📅 昨天</a></li>';
        html += '<li><a href="#" onclick="DailyAnalysis.setDateRange(\'thisweek\');$(\'#daDateDropdown\').hide();return false;" style="display:block;text-decoration:none;">📆 本周</a></li>';
        html += '<li><a href="#" onclick="DailyAnalysis.setDateRange(\'lastweek\');$(\'#daDateDropdown\').hide();return false;" style="display:block;text-decoration:none;">📆 上周</a></li>';
        html += '<li style="height:1px;background:#e2e8f0;margin:4px 0;"></li>';
        html += '<li><a href="#" onclick="DailyAnalysis.setDateRange(\'thismonth\');$(\'#daDateDropdown\').hide();return false;" style="display:block;text-decoration:none;">🗓️ 本月</a></li>';
        html += '<li><a href="#" onclick="DailyAnalysis.setDateRange(\'lastmonth\');$(\'#daDateDropdown\').hide();return false;" style="display:block;text-decoration:none;">🗓️ 上月</a></li>';
        html += '</ul>';
        html += '</div>';

        // 分隔线
        html += '<div style="width:1px;height:44px;background:#e2e8f0;"></div>';

        // 动态筛选条件（和其他内容放同一行）
        html += '<div style="display:flex;flex-wrap:wrap;align-items:flex-end;gap:12px;" id="dynamicFilters">';
        html += '</div>';

        // 分隔线
        html += '<div style="width:1px;height:44px;background:#e2e8f0;"></div>';

        // 查询 + 清除按钮（放在最后）
        html += '<div style="display:flex;gap:8px;">';
        html += '<button class="da-btn-primary" onclick="DailyAnalysis.runDailyAnalysis()" style="height:38px;padding:0 20px;font-size:0.85rem;border-radius:10px;">🔍 查询统计</button>';
        html += '<button class="da-btn-secondary" onclick="DailyAnalysis.clearAllFilters()" style="height:38px;padding:0 20px;font-size:0.85rem;border-radius:10px;">✖ 清除筛选</button>';
        html += '</div>';

        html += '</div>';

        html += '</div>';

        // ===== 统计数字卡片 =====
        html += '<div class="da-stat-grid">';
        html += '<div class="da-stat-card">';
        html += '<div class="da-stat-label">📊 阳性率</div>';
        html += '<div class="da-stat-value" id="positiveRate">-</div>';
        html += '</div>';
        html += '<div class="da-stat-card">';
        html += '<div class="da-stat-label">📈 总检查数</div>';
        html += '<div class="da-stat-value" id="totalCount">-</div>';
        html += '</div>';
        html += '<div class="da-stat-card">';
        html += '<div class="da-stat-label">🔴 阳性数</div>';
        html += '<div class="da-stat-value" id="positiveCount">-</div>';
        html += '</div>';
        html += '<div class="da-stat-card">';
        html += '<div class="da-stat-label">🟢 阴性数</div>';
        html += '<div class="da-stat-value" id="negativeCount">-</div>';
        html += '</div>';
        html += '</div>';

        // ===== 分析结果区域 =====
        html += '<div class="da-result-card">';
        html += '<div style="display:flex;flex-wrap:wrap;align-items:center;justify-content:space-between;gap:12px;margin-bottom:20px;">';
        html += '<h5 style="margin:0;">📋 统计分析结果</h5>';
        html += '<div class="da-sub-btn-group">';
        html += '<button class="da-sub-btn" onclick="DailyAnalysis.runAnalysis(\'department\')">🏥 科室统计</button>';
        html += '<button class="da-sub-btn" onclick="DailyAnalysis.runAnalysis(\'doctor\')">👨⚕️ 医生统计</button>';
        html += '<button class="da-sub-btn" onclick="DailyAnalysis.runAnalysis(\'category\')">🔬 检查类型</button>';
        html += '</div>';
        html += '</div>';
        html += '<div id="analysisResult"><div class="da-empty-state">📭 请选择日期范围并点击查询</div></div>';
        html += '</div>';

        // ===== 图表区域 =====
        html += '<div class="da-chart-card" id="chartSection" style="display:none;">';
        html += '<h5>📊 统计图表</h5>';
        html += '<div id="chartContainer" style="height:420px;"></div>';
        html += '</div>';

        html += '</div>';

        // 下拉菜单切换
        html += '<script>';
        html += '$("#daDateDropdownBtn").on("click",function(e){e.stopPropagation();$("#daDateDropdown").toggle();});';
        html += '$(document).on("click",function(){$("#daDateDropdown").hide();});';
        html += '<\/script>';

        $('#panelContent').html(html);

        var today = new Date();
        var firstDay = new Date(today.getFullYear(), today.getMonth(), 1);
        $('#startDate').val(firstDay.toISOString().split('T')[0]);
        $('#endDate').val(today.toISOString().split('T')[0]);

        function applyFilters() {
            generateDynamicFilters();
            loadAllFieldOptions();
        }

        if (isDataLoaded) {
            applyFilters();
        } else {
            initCallbacks.push(applyFilters);
            loadAllOptions(function() {
                loadQueryConfig(applyFilters);
            });
        }
    }

    return {
        init: init,
        runDailyAnalysis: runDailyAnalysis,
        runAnalysis: runAnalysis,
        sortByColumn: sortByColumn,
        clearAllFilters: clearAllFilters,
        setDateRange: setDateRange,
        onParentFieldChanged: onParentFieldChanged
    };
})();