var DailyAnalysis = (function() {
    var queryConfig = [];
    var sortColumn = '';
    var sortDirection = 'DESC';
    var chartInstance = null;
    var isDataLoaded = false;
    var initCallbacks = [];
    var currentData = [];
    var columnOrder = [];
    var draggedColumn = null;

    function preloadData() {
        loadAllOptions(function() {
            isDataLoaded = true;
            initCallbacks.forEach(function(cb) { cb(); });
            initCallbacks = [];
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

    function loadAllOptions(callback) {
        $.ajax({
            url: '/get-all-options',
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

    function onInit(callback) {
        if (isDataLoaded) {
            callback();
        } else {
            initCallbacks.push(callback);
        }
    }

    function init() {
        renderAnalysisPanel();
        onInit(function() {
            initControls();
        });
    }

    function renderAnalysisPanel() {
        var html = `
            <style>
                .daily-analysis-container { padding: 15px; margin: 0; width: 100%; }
                
                .query-panel { 
                    background: white; 
                    padding: 16px; 
                    border-radius: 12px; 
                    box-shadow: 0 4px 16px rgba(0,0,0,0.08); 
                    margin-bottom: 16px; 
                }
                
                .query-header {
                    display: flex;
                    align-items: center;
                    justify-content: space-between;
                    margin-bottom: 12px;
                    padding-bottom: 12px;
                    border-bottom: 1px solid #e5e7eb;
                }
                
                .date-range {
                    display: flex;
                    align-items: center;
                    gap: 8px;
                }
                
                .date-range label {
                    color: #666;
                    font-size: 13px;
                    font-weight: 500;
                }
                
                .date-input {
                    padding: 6px 10px;
                    border: 1px solid #d1d5db;
                    border-radius: 6px;
                    font-size: 13px;
                }
                
                .quick-select {
                    padding: 6px 14px;
                    border: 1px solid #d1d5db;
                    border-radius: 6px;
                    font-size: 13px;
                    cursor: pointer;
                    background: white;
                }
                
                .query-btn {
                    padding: 8px 24px;
                    background: linear-gradient(135deg, #3b82f6 0%, #2563eb 100%);
                    color: white;
                    border: none;
                    border-radius: 6px;
                    cursor: pointer;
                    font-size: 13px;
                    font-weight: 500;
                }
                
                .clear-btn {
                    padding: 8px 16px;
                    background: #f3f4f6;
                    color: #374151;
                    border: 1px solid #d1d5db;
                    border-radius: 6px;
                    cursor: pointer;
                    font-size: 13px;
                    margin-left: 8px;
                }
                
                .filter-grid {
                    display: grid;
                    grid-template-columns: repeat(8, 1fr);
                    gap: 12px;
                }
                
                .filter-item {
                    display: flex;
                    flex-direction: column;
                    gap: 4px;
                }
                
                .filter-item label {
                    font-size: 12px;
                    color: #666;
                    font-weight: 500;
                }
                
                .filter-select {
                    padding: 6px 8px;
                    border: 1px solid #d1d5db;
                    border-radius: 4px;
                    font-size: 12px;
                    background: white;
                }
                
                .filter-search {
                    width: 100%;
                    padding: 4px 6px;
                    border: 1px solid #d1d5db;
                    border-radius: 4px;
                    font-size: 12px;
                    margin-bottom: 4px;
                    box-sizing: border-box;
                }
                
                .filter-options {
                    max-height: 200px;
                    overflow-y: auto;
                    border: 1px solid #d1d5db;
                    border-top: none;
                    border-radius: 0 0 4px 4px;
                    background: white;
                    position: absolute;
                    z-index: 100;
                    width: 100%;
                    display: none;
                }
                
                .filter-option {
                    padding: 6px 8px;
                    cursor: pointer;
                    font-size: 12px;
                }
                
                .filter-option:hover {
                    background: #f3f4f6;
                }
                
                .filter-option.selected {
                    background: #3b82f6;
                    color: white;
                }
                
                .filter-wrapper {
                    position: relative;
                }

                .stats-grid { 
                    display: grid; 
                    grid-template-columns: repeat(4, 1fr); 
                    gap: 12px; 
                    margin-bottom: 16px; 
                }
                .stat-card { 
                    background: white; 
                    padding: 16px; 
                    border-radius: 10px; 
                    box-shadow: 0 4px 16px rgba(0,0,0,0.06); 
                    display: flex;
                    align-items: center;
                    gap: 12px;
                }
                .stat-icon { 
                    width: 40px; 
                    height: 40px; 
                    border-radius: 10px; 
                    display: flex; 
                    align-items: center; 
                    justify-content: center; 
                    font-size: 18px; 
                }
                .stat-info {
                    flex: 1;
                }
                .stat-label { color: #6b7280; font-size: 12px; margin-bottom: 2px; }
                .stat-value { font-size: 1.5rem; font-weight: bold; }
                .stat-positive { color: #dc2626; }
                .stat-negative { color: #16a34a; }
                .stat-total { color: #1a73e8; }
                .stat-rate { color: #f59e0b; }

                .result-panel { 
                    background: white; 
                    border-radius: 12px; 
                    box-shadow: 0 4px 16px rgba(0,0,0,0.06); 
                    overflow: hidden; 
                }
                .panel-header { 
                    padding: 14px 16px; 
                    background: #f8fafc; 
                    border-bottom: 1px solid #e5e7eb; 
                    display: flex; 
                    justify-content: space-between; 
                    align-items: center; 
                }
                .panel-header h2 { font-size: 14px; color: #1f2937; font-weight: 600; margin: 0; }
                .export-btn { 
                    padding: 4px 12px; 
                    background: #1a73e8; 
                    color: white; 
                    border: none; 
                    border-radius: 4px; 
                    font-size: 12px; 
                    cursor: pointer; 
                }

                .table-container { overflow-x: auto; max-height: 450px; overflow-y: auto; }
                .result-table { width: 100%; border-collapse: collapse; }
                .result-table th { 
                    background: #f8fafc; 
                    padding: 10px 12px; 
                    text-align: left; 
                    font-weight: 600; 
                    color: #374151; 
                    font-size: 12px; 
                    white-space: nowrap; 
                    position: sticky; 
                    top: 0; 
                    z-index: 10;
                    cursor: pointer;
                }
                .result-table td { padding: 10px 12px; border-bottom: 1px solid #f3f4f6; font-size: 13px; }
                .result-table tr:hover { background: #f9fafb; }
                .rate-high { color: #dc2626; font-weight: 600; }
                .rate-low { color: #16a34a; font-weight: 600; }

                .loading { text-align: center; padding: 40px; color: #9ca3af; }
                .loading-icon { font-size: 2rem; margin-bottom: 8px; }

                .chart-container { 
                    background: white; 
                    padding: 16px; 
                    border-radius: 12px; 
                    box-shadow: 0 4px 16px rgba(0,0,0,0.06); 
                    height: 350px; 
                    margin-top: 16px;
                }
            </style>

            <div class="daily-analysis-container">
                <div class="query-panel">
                    <div class="query-header">
                        <div class="date-range">
                            <label>日期范围</label>
                            <input type="date" id="startDate" value="2026-03-31" class="date-input">
                            <span style="color:#9ca3af;">至</span>
                            <input type="date" id="endDate" value="2026-04-30" class="date-input">
                            <select class="quick-select" onchange="DailyAnalysis.quickQuery(this.value)">
                                <option value="">⚙ 快速选择</option>
                                <option value="today">今天</option>
                                <option value="yesterday">昨天</option>
                                <option value="week">本周</option>
                                <option value="month">本月</option>
                                <option value="lastMonth">上月</option>
                            </select>
                        </div>
                        <div>
                            <button class="query-btn" onclick="DailyAnalysis.queryData()">
                                查询统计
                            </button>
                            <button class="clear-btn" onclick="DailyAnalysis.resetForm()">
                                ✕ 清除筛选
                            </button>
                        </div>
                    </div>

                    <div class="filter-grid">
                        <div class="filter-item">
                            <label>系统</label>
                            <div class="filter-wrapper">
                                <input type="text" id="systemSearch" class="filter-search" placeholder="搜索系统...">
                                <div id="systemOptions" class="filter-options"></div>
                            </div>
                            <input type="hidden" id="system">
                        </div>
                        <div class="filter-item">
                            <label>报告医生</label>
                            <div class="filter-wrapper">
                                <input type="text" id="reporterSearch" class="filter-search" placeholder="搜索医生...">
                                <div id="reporterOptions" class="filter-options"></div>
                            </div>
                            <input type="hidden" id="reporter">
                        </div>
                        <div class="filter-item">
                            <label>审核医生</label>
                            <div class="filter-wrapper">
                                <input type="text" id="reviewerSearch" class="filter-search" placeholder="搜索医生...">
                                <div id="reviewerOptions" class="filter-options"></div>
                            </div>
                            <input type="hidden" id="reviewer">
                        </div>
                        <div class="filter-item">
                            <label>技师</label>
                            <div class="filter-wrapper">
                                <input type="text" id="technicianSearch" class="filter-search" placeholder="搜索技师...">
                                <div id="technicianOptions" class="filter-options"></div>
                            </div>
                            <input type="hidden" id="technician">
                        </div>
                        <div class="filter-item">
                            <label>执行科室</label>
                            <div class="filter-wrapper">
                                <input type="text" id="departmentSearch" class="filter-search" placeholder="搜索科室...">
                                <div id="departmentOptions" class="filter-options"></div>
                            </div>
                            <input type="hidden" id="department">
                        </div>
                        <div class="filter-item">
                            <label>检查类型</label>
                            <div class="filter-wrapper">
                                <input type="text" id="categorySearch" class="filter-search" placeholder="搜索类型...">
                                <div id="categoryOptions" class="filter-options"></div>
                            </div>
                            <input type="hidden" id="category">
                        </div>
                        <div class="filter-item">
                            <label>病人类型</label>
                            <div class="filter-wrapper">
                                <input type="text" id="patientTypeSearch" class="filter-search" placeholder="搜索类型...">
                                <div id="patientTypeOptions" class="filter-options"></div>
                            </div>
                            <input type="hidden" id="patientType">
                        </div>
                        <div class="filter-item">
                            <label>阴阳性</label>
                            <div class="filter-wrapper">
                                <input type="text" id="resultStatusSearch" class="filter-search" placeholder="搜索状态...">
                                <div id="resultStatusOptions" class="filter-options"></div>
                            </div>
                            <input type="hidden" id="resultStatus">
                        </div>
                    </div>
                </div>

                <div class="stats-grid">
                    <div class="stat-card">
                        <div class="stat-icon" style="background: rgba(234, 179, 8, 0.15); color: #f59e0b;">📊</div>
                        <div class="stat-info">
                            <div class="stat-label">阳性率</div>
                            <div class="stat-value stat-rate" id="positiveRate">-</div>
                        </div>
                    </div>
                    <div class="stat-card">
                        <div class="stat-icon" style="background: rgba(59, 130, 246, 0.15); color: #3b82f6;">📈</div>
                        <div class="stat-info">
                            <div class="stat-label">总检查数</div>
                            <div class="stat-value stat-total" id="totalCount">-</div>
                        </div>
                    </div>
                    <div class="stat-card">
                        <div class="stat-icon" style="background: rgba(220, 38, 38, 0.15); color: #dc2626;">🔴</div>
                        <div class="stat-info">
                            <div class="stat-label">阳性数</div>
                            <div class="stat-value stat-positive" id="positiveCount">-</div>
                        </div>
                    </div>
                    <div class="stat-card">
                        <div class="stat-icon" style="background: rgba(22, 163, 74, 0.15); color: #16a34a;">🟢</div>
                        <div class="stat-info">
                            <div class="stat-label">阴性数</div>
                            <div class="stat-value stat-negative" id="negativeCount">-</div>
                        </div>
                    </div>
                </div>

                <div class="result-panel">
                    <div class="panel-header">
                        <h2>📋 统计分析结果</h2>
                        <div style="display: flex; gap: 8px;">
                            <button class="export-btn" onclick="DailyAnalysis.getDepartmentStatistics()">📊 科室统计</button>
                            <button class="export-btn" onclick="DailyAnalysis.getDoctorStatistics()">👨⚕️ 医生统计</button>
                            <button class="export-btn" onclick="DailyAnalysis.getCategoryStatistics()">📋 检查类型</button>
                            <button class="export-btn" onclick="DailyAnalysis.exportData()">📥 导出数据</button>
                        </div>
                    </div>
                    <div class="table-container">
                        <div id="analysisResult" class="loading">
                            <div class="loading-icon">⏳</div>
                            <p>点击查询按钮加载数据...</p>
                        </div>
                    </div>
                </div>

                <div class="chart-container">
                    <div id="chartContainer" style="height: 100%;"></div>
                </div>
            </div>
        `;
        $('#panelContent').html(html);
    }

    function initControls() {
        $('#queryForm').on('submit', function(e) {
            e.preventDefault();
            queryData();
        });
        populateDropdowns();
    }

    var currentDropdowns = {};
    
    function getPinyin(chars) {
        var pinyinMap = {
            'a':'a','b':'b','c':'c','d':'d','e':'e','f':'f','g':'g','h':'h',
            'j':'j','k':'k','l':'l','m':'m','n':'n','o':'o','p':'p','q':'q',
            'r':'r','s':'s','t':'t','u':'u','v':'v','w':'w','x':'x','y':'y','z':'z'
        };
        var result = '';
        for (var i = 0; i < chars.length; i++) {
            var c = chars.charAt(i);
            var lower = c.toLowerCase();
            result += pinyinMap[lower] || c;
        }
        return result;
    }
    
    function initSearchableDropdown(id, options, displayField, valueField, allLabel) {
        var searchInput = $('#' + id + 'Search');
        var optionsDiv = $('#' + id + 'Options');
        var hiddenInput = $('#' + id);
        
        currentDropdowns[id] = {
            options: options,
            displayField: displayField,
            valueField: valueField,
            allLabel: allLabel,
            selectedValue: '',
            selectedDisplay: allLabel
        };
        
        searchInput.val(allLabel);
        hiddenInput.val('');
        
        searchInput.on('focus', function() {
            renderDropdownOptions(id, '');
            optionsDiv.show();
        });
        
        searchInput.on('input', function() {
            var searchTerm = $(this).val();
            if (searchTerm === allLabel) searchTerm = '';
            renderDropdownOptions(id, searchTerm);
            optionsDiv.show();
        });
        
        $(document).on('click', function(e) {
            if (!$(e.target).closest('.filter-wrapper').length) {
                $('.filter-options').hide();
            }
        });
    }
    
    function renderDropdownOptions(id, searchTerm) {
        var config = currentDropdowns[id];
        var optionsDiv = $('#' + id + 'Options');
        var html = '<div class="filter-option" data-value="" data-display="' + config.allLabel + '">' + config.allLabel + '</div>';
        
        config.options.forEach(function(item) {
            var display = typeof item === 'string' ? item : item[config.displayField];
            var value = typeof item === 'string' ? item : item[config.valueField];
            
            var searchLower = searchTerm.toLowerCase();
            var displayLower = display.toLowerCase();
            var pinyin = getPinyin(display);
            
            if (searchTerm === '' || displayLower.includes(searchLower) || pinyin.includes(searchLower)) {
                var selectedClass = value === config.selectedValue ? 'selected' : '';
                html += '<div class="filter-option ' + selectedClass + '" data-value="' + htmlEncode(value) + '" data-display="' + htmlEncode(display) + '">' + htmlEncode(display) + '</div>';
            }
        });
        
        optionsDiv.html(html);
        
        optionsDiv.find('.filter-option').on('click', function() {
            var value = $(this).data('value');
            var display = $(this).data('display');
            config.selectedValue = value;
            config.selectedDisplay = display;
            $('#' + id + 'Search').val(display);
            $('#' + id).val(value);
            $(this).parent().hide();
        });
    }
    
    function populateDropdowns() {
        if (!queryConfig || typeof queryConfig !== 'object') {
            console.warn('queryConfig not loaded');
            return;
        }

        if (queryConfig.systems) {
            initSearchableDropdown('system', queryConfig.systems, 'name', 'code', '全部系统');
        }

        if (queryConfig.reporters) {
            initSearchableDropdown('reporter', queryConfig.reporters, '', '', '全部');
        }

        if (queryConfig.reviewers) {
            initSearchableDropdown('reviewer', queryConfig.reviewers, '', '', '全部');
        }

        if (queryConfig.technicians) {
            initSearchableDropdown('technician', queryConfig.technicians, '', '', '全部');
        }

        if (queryConfig.departments) {
            initSearchableDropdown('department', queryConfig.departments, '', '', '全部');
        }

        if (queryConfig.categories) {
            initSearchableDropdown('category', queryConfig.categories, '', '', '全部');
        }

        if (queryConfig.patientTypes) {
            initSearchableDropdown('patientType', queryConfig.patientTypes, 'name', 'code', '全部');
        }

        if (queryConfig.resultStatus) {
            initSearchableDropdown('resultStatus', queryConfig.resultStatus, 'name', 'code', '全部');
        }
    }

    function setDefaultDates() {
        var today = new Date();
        var firstDay = new Date(today.getFullYear(), today.getMonth(), 1);
        $('#startDate').val(firstDay.toISOString().split('T')[0]);
        $('#endDate').val(today.toISOString().split('T')[0]);
    }

    function resetForm() {
        $('#startDate').val('2026-03-31');
        $('#endDate').val('2026-04-30');
        
        var dropdowns = ['system', 'patientType', 'reporter', 'reviewer', 'technician', 'department', 'category', 'resultStatus'];
        dropdowns.forEach(function(id) {
            $('#' + id).val('');
            var config = currentDropdowns[id];
            if (config) {
                config.selectedValue = '';
                config.selectedDisplay = config.allLabel;
                $('#' + id + 'Search').val(config.allLabel);
            }
        });
        
        sortColumn = '';
        sortDirection = 'DESC';
        currentData = [];
        columnOrder = [];
        $('#analysisResult').html('<div class="loading"><div class="loading-icon">⏳</div><p>点击查询按钮加载数据...</p></div>');
        resetStatistics();
    }

    function quickQuery(type) {
        var today = new Date();
        var startDate, endDate;

        switch(type) {
            case 'today':
                startDate = today;
                endDate = today;
                break;
            case 'yesterday':
                startDate = new Date(today.getTime() - 24 * 60 * 60 * 1000);
                endDate = startDate;
                break;
            case 'week':
                startDate = new Date(today);
                startDate.setDate(today.getDate() - today.getDay());
                endDate = today;
                break;
            case 'month':
                startDate = new Date(today.getFullYear(), today.getMonth(), 1);
                endDate = today;
                break;
            case 'lastMonth':
                startDate = new Date(today.getFullYear(), today.getMonth() - 1, 1);
                endDate = new Date(today.getFullYear(), today.getMonth(), 0);
                break;
        }

        if (startDate) {
            $('#startDate').val(startDate.toISOString().split('T')[0]);
            $('#endDate').val(endDate.toISOString().split('T')[0]);
            queryData();
        }
    }

    function quickFilter(type) {
        switch(type) {
            case 'positive':
                $('#resultStatus').val('383927');
                break;
            case 'negative':
                $('#resultStatus').val('383926');
                break;
            case 'all':
                $('#resultStatus').val('');
                break;
        }
        queryData();
    }

    function queryData() {
        var data = {
            startDate: $('#startDate').val(),
            endDate: $('#endDate').val(),
            system: $('#system').val(),
            reporter: $('#reporter').val() || '',
            reviewer: $('#reviewer').val() || '',
            technician: $('#technician').val() || '',
            department: $('#department').val() || '',
            category: $('#category').val() || '',
            patientType: $('#patientType').val(),
            resultStatus: $('#resultStatus').val() || '',
            sortBy: sortColumn || '任务数量',
            sortOrder: sortDirection,
            pageSize: 0,
            pageIndex: 1
        };

        if (!data.startDate || !data.endDate) {
            ErrorHandler.show('请选择日期范围', 'warning');
            return;
        }

        $('#analysisResult').html('<div class="loading"><div class="loading-icon">⏳</div><p>数据加载中...</p></div>');

        $.ajax({
            url: '/daily-analysis',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            success: function(response) {
                try {
                    var resultData = ErrorHandler.validateResponse(response);
                    if (resultData) {
                        currentData = resultData;
                        columnOrder = resultData.length > 0 ? Object.keys(resultData[0]) : [];
                        displayAnalysisResult(resultData);
                        updateStatistics(resultData);
                        try {
                            renderChart(resultData);
                        } catch (e) {
                            console.warn('图表渲染失败:', e);
                        }
                    } else {
                        resetStatistics();
                    }
                } catch (e) {
                    console.error('数据处理失败:', e);
                    $('#analysisResult').html('<div class="alert alert-danger">数据处理失败: ' + e.message + '</div>');
                    resetStatistics();
                }
            },
            error: function(xhr, status, error) {
                ErrorHandler.handleAjaxError(xhr, status, error);
                $('#analysisResult').html('<div class="loading"><div class="loading-icon">❌</div><p>加载失败</p></div>');
                resetStatistics();
            }
        });
    }

    function sortData(column) {
        if (!currentData || currentData.length === 0) return;

        if (sortColumn === column) {
            sortDirection = sortDirection === 'ASC' ? 'DESC' : 'ASC';
        } else {
            sortColumn = column;
            sortDirection = 'ASC';
        }

        var isNumeric = ['任务数量', '阳性数量', '阴性数量', '阳性率'].indexOf(column) !== -1;

        currentData.sort(function(a, b) {
            var valA = a[column];
            var valB = b[column];

            if (isNumeric) {
                valA = parseFloat(valA) || 0;
                valB = parseFloat(valB) || 0;
            } else {
                valA = String(valA || '').toLowerCase();
                valB = String(valB || '').toLowerCase();
            }

            if (valA < valB) return sortDirection === 'ASC' ? -1 : 1;
            if (valA > valB) return sortDirection === 'ASC' ? 1 : -1;
            return 0;
        });

        displayAnalysisResult(currentData);
    }

    function displayAnalysisResult(data) {
        if (!data || data.length === 0) {
            $('#analysisResult').html('<div class="loading"><div class="loading-icon">📭</div><p>没有查询到数据</p></div>');
            return;
        }

        var columns = columnOrder.length > 0 ? columnOrder : Object.keys(data[0]);
        var colCount = columns.length;
        var rowCount = data.length;

        var html = [];
        html.push('<table class="result-table">');
        html.push('<thead><tr>');

        for (var i = 0; i < colCount; i++) {
            var col = columns[i];
            var sortIcon = sortColumn === col ? (sortDirection === 'ASC' ? ' ▲' : ' ▼') : '';
            html.push('<th onclick="DailyAnalysis.sortData(\'' + col + '\')" title="点击排序">' + htmlEncode(col) + sortIcon + '</th>');
        }

        html.push('</tr></thead><tbody>');

        for (var i = 0; i < rowCount; i++) {
            var row = data[i];
            var rowHtml = '<tr>';
            for (var j = 0; j < colCount; j++) {
                var col = columns[j];
                var value = row[col];
                var systemMap = {
                    'UIS': '超声', 'RIS': '放射', 'EIS': '内镜',
                    'PIS': '病理', 'NMS': '核医学'
                };
                if (col === '阳性率') {
                    var rateValue = parseFloat(value) || 0;
                    var rateStr = rateValue.toFixed(2) + '%';
                    var system = row['系统'] || '';
                    var displaySystem = systemMap[system] || system;
                    var threshold = 50;
                    if (system === 'RIS' || displaySystem === '放射') {
                        threshold = 60;
                    }
                    var rateClass = rateValue >= threshold ? 'rate-high' : 'rate-low';
                    rowHtml += '<td><span class="' + rateClass + '">' + rateStr + '</span></td>';
                } else if (col === '系统') {
                    var displayValue = systemMap[value] || value;
                    rowHtml += '<td>' + htmlEncode(displayValue) + '</td>';
                } else if (col === '病人类型') {
                    var displayValue = value;
                    var patientTypeMap = {
                        '1': '门诊', '2': '住院', '3': '急诊', '4': '体检',
                        '138138': '门诊', '138139': '急诊', '138140': '体检', '145235': '住院',
                        'OPD': '门诊', 'IPD': '住院', 'EMER': '急诊', 'CHECKUP': '体检'
                    };
                    if (patientTypeMap[displayValue]) {
                        displayValue = patientTypeMap[displayValue];
                    }
                    rowHtml += '<td>' + htmlEncode(displayValue) + '</td>';
                } else if (col === '结果状态') {
                    var displayValue = value;
                    var resultStatusMap = {
                        '383927': '阳性', '383926': '阴性',
                        'P': '阳性', 'N': '阴性', 'Y': '阳性',
                        '阳性': '阳性', '阴性': '阴性'
                    };
                    if (resultStatusMap[displayValue]) {
                        displayValue = resultStatusMap[displayValue];
                    }
                    rowHtml += '<td>' + htmlEncode(displayValue) + '</td>';
                } else {
                    rowHtml += '<td>' + htmlEncode(value !== null && value !== undefined ? value : '') + '</td>';
                }
            }
            rowHtml += '</tr>';
            html.push(rowHtml);
        }

        html.push('</tbody></table>');
        html.push('<div style="padding:10px;color:#64748b;font-size:0.8rem;">共 ' + rowCount + ' 条记录（点击列头排序）</div>');

        $('#analysisResult').html(html.join(''));
    }

    function onColumnDragStart(event, column) {
        draggedColumn = column;
        event.dataTransfer.effectAllowed = 'move';
        event.target.style.opacity = '0.5';
    }

    function onColumnDragOver(event) {
        event.preventDefault();
        event.dataTransfer.dropEffect = 'move';
    }

    function onColumnDrop(event, targetColumn) {
        event.preventDefault();
        event.target.style.opacity = '1';

        if (draggedColumn && draggedColumn !== targetColumn && columnOrder.length > 0) {
            var draggedIndex = columnOrder.indexOf(draggedColumn);
            var targetIndex = columnOrder.indexOf(targetColumn);

            if (draggedIndex !== -1 && targetIndex !== -1) {
                columnOrder.splice(draggedIndex, 1);
                columnOrder.splice(targetIndex, 0, draggedColumn);
                displayAnalysisResult(currentData);
            }
        }

        draggedColumn = null;
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

    function exportData() {
        if (!currentData || currentData.length === 0) {
            ErrorHandler.show('没有数据可导出', 'warning');
            return;
        }

        var columns = columnOrder.length > 0 ? columnOrder : Object.keys(currentData[0]);
        var csvContent = '\uFEFF' + columns.join(',') + '\n';

        currentData.forEach(function(row) {
            var rowData = [];
            columns.forEach(function(col) {
                var value = row[col];
                if (col === '阳性率') {
                    value = (parseFloat(value) || 0).toFixed(2) + '%';
                } else if (col === '系统') {
                    var systemMap = { 'UIS': '超声', 'RIS': '放射', 'EIS': '内镜', 'PIS': '病理', 'NMS': '核医学' };
                    value = systemMap[value] || value;
                } else if (col === '病人类型') {
                    var patientTypeMap = {
                        '1': '门诊', '2': '住院', '3': '急诊', '4': '体检',
                        '138138': '门诊', '138139': '急诊', '138140': '体检', '145235': '住院',
                        'OPD': '门诊', 'IPD': '住院', 'EMER': '急诊', 'CHECKUP': '体检'
                    };
                    value = patientTypeMap[value] || value;
                }
                value = String(value || '');
                rowData.push(value.includes(',') ? '"' + value + '"' : value);
            });
            csvContent += rowData.join(',') + '\n';
        });

        var blob = new Blob([csvContent], { type: 'text/csv;charset=utf-8;' });
        var link = document.createElement('a');
        var url = URL.createObjectURL(blob);
        link.setAttribute('href', url);
        link.setAttribute('download', '每日分析_' + $('#startDate').val() + '_' + $('#endDate').val() + '.csv');
        link.style.visibility = 'hidden';
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
    }

    function getDepartmentStatistics() {
        var data = {
            startDate: $('#startDate').val(),
            endDate: $('#endDate').val(),
            system: $('#system').val() || ''
        };

        if (!data.startDate || !data.endDate) {
            ErrorHandler.show('请选择日期范围', 'warning');
            return;
        }

        $('#analysisResult').html('<div class="loading"><div class="loading-icon">⏳</div><p>数据加载中...</p></div>');

        $.ajax({
            url: '/department-statistics',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            success: function(response) {
                var resultData = ErrorHandler.validateResponse(response);
                if (resultData) {
                    currentData = resultData;
                    columnOrder = resultData.length > 0 ? Object.keys(resultData[0]) : [];
                    displayAnalysisResult(resultData);
                    updateStatistics(resultData);
                }
            },
            error: ErrorHandler.handleAjaxError
        });
    }

    function getDoctorStatistics() {
        var data = {
            startDate: $('#startDate').val(),
            endDate: $('#endDate').val(),
            system: $('#system').val() || ''
        };

        if (!data.startDate || !data.endDate) {
            ErrorHandler.show('请选择日期范围', 'warning');
            return;
        }

        $('#analysisResult').html('<div class="loading"><div class="loading-icon">⏳</div><p>数据加载中...</p></div>');

        $.ajax({
            url: '/doctor-statistics',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            success: function(response) {
                var resultData = ErrorHandler.validateResponse(response);
                if (resultData) {
                    currentData = resultData;
                    columnOrder = resultData.length > 0 ? Object.keys(resultData[0]) : [];
                    displayAnalysisResult(resultData);
                    updateStatistics(resultData);
                }
            },
            error: ErrorHandler.handleAjaxError
        });
    }

    function getCategoryStatistics() {
        var data = {
            startDate: $('#startDate').val(),
            endDate: $('#endDate').val(),
            system: $('#system').val() || ''
        };

        if (!data.startDate || !data.endDate) {
            ErrorHandler.show('请选择日期范围', 'warning');
            return;
        }

        $('#analysisResult').html('<div class="loading"><div class="loading-icon">⏳</div><p>数据加载中...</p></div>');

        $.ajax({
            url: '/category-statistics',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            success: function(response) {
                var resultData = ErrorHandler.validateResponse(response);
                if (resultData) {
                    currentData = resultData;
                    columnOrder = resultData.length > 0 ? Object.keys(resultData[0]) : [];
                    displayAnalysisResult(resultData);
                    updateStatistics(resultData);
                }
            },
            error: ErrorHandler.handleAjaxError
        });
    }

    function renderChart(data) {
        if (!data || data.length === 0) return;

        var container = document.getElementById('chartContainer');
        if (!container) return;

        if (chartInstance) {
            chartInstance.dispose();
        }

        chartInstance = echarts.init(container);

        var systemData = {};
        data.forEach(function(row) {
            var system = row['系统'] || '未知';
            var systemMap = { 'UIS': '超声', 'RIS': '放射', 'EIS': '内镜', 'PIS': '病理', 'NMS': '核医学' };
            system = systemMap[system] || system;
            
            if (!systemData[system]) {
                systemData[system] = { positive: 0, negative: 0, total: 0 };
            }
            systemData[system].positive += parseInt(row['阳性数量']) || 0;
            systemData[system].negative += parseInt(row['阴性数量']) || 0;
            systemData[system].total += parseInt(row['任务数量']) || 0;
        });

        var systems = Object.keys(systemData);
        var positiveData = systems.map(function(s) { return systemData[s].positive; });
        var negativeData = systems.map(function(s) { return systemData[s].negative; });
        var rateData = systems.map(function(s) { 
            return systemData[s].total > 0 ? (systemData[s].positive / systemData[s].total * 100) : 0; 
        });

        var option = {
            title: { text: '各系统检查结果统计', left: 'center', textStyle: { fontSize: 14 } },
            tooltip: { trigger: 'axis' },
            legend: { data: ['阳性', '阴性', '阳性率'], bottom: 10 },
            grid: { left: '3%', right: '4%', bottom: '15%', top: '15%', containLabel: true },
            xAxis: { type: 'category', data: systems, axisLabel: { rotate: 30, fontSize: 12 } },
            yAxis: [
                { type: 'value', name: '数量', fontSize: 12 },
                { type: 'value', name: '阳性率(%)', min: 0, max: 100, fontSize: 12 }
            ],
            series: [
                { name: '阳性', type: 'bar', data: positiveData, itemStyle: { color: '#ef4444' } },
                { name: '阴性', type: 'bar', data: negativeData, itemStyle: { color: '#22c55e' } },
                { name: '阳性率', type: 'line', yAxisIndex: 1, data: rateData, lineStyle: { color: '#f59e0b', width: 3 } }
            ]
        };

        chartInstance.setOption(option);
        window.addEventListener('resize', function() { chartInstance && chartInstance.resize(); });
    }

    function htmlEncode(str) {
        if (str === null || str === undefined) return '';
        if (typeof str !== 'string') return String(str);
        return str.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/"/g, '&quot;');
    }

    return {
        init: init,
        queryData: queryData,
        sortData: sortData,
        onColumnDragStart: onColumnDragStart,
        onColumnDragOver: onColumnDragOver,
        onColumnDrop: onColumnDrop,
        resetForm: resetForm,
        exportData: exportData,
        quickQuery: quickQuery,
        quickFilter: quickFilter,
        getDepartmentStatistics: getDepartmentStatistics,
        getDoctorStatistics: getDoctorStatistics,
        getCategoryStatistics: getCategoryStatistics
    };
})();

$(document).ready(function() {
    DailyAnalysis.init();
});