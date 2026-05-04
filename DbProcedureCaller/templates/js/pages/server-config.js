var ServerConfig = {
    init: function() {
        this.loadCurrentPort();
    },

    loadCurrentPort: function() {
        $.ajax({
            url: '/get-port',
            type: 'GET',
            success: function(response) {
                try {
                    var resp = typeof response === 'string' ? JSON.parse(response) : response;
                    if (resp.success) {
                        $('#currentPortDisplay').text(resp.data.port);
                    }
                } catch (e) {
                    console.error('加载端口失败');
                }
            }
        });
    },

    showDbModal: function() {
        $('#dbModalResult').html('');
        $('#dbModalStatus').text('加载配置中...');
        document.getElementById('dbModal').style.display = 'flex';
        document.body.style.overflow = 'hidden';
        
        $.ajax({
            url: '/get-db-config',
            type: 'GET',
            success: function(response) {
                try {
                    var resp = typeof response === 'string' ? JSON.parse(response) : response;
                    if (resp.success) {
                        $('#dbModalServer').val(resp.data.server || '');
                        $('#dbModalName').val(resp.data.database || '');
                        $('#dbModalUser').val(resp.data.username || '');
                        $('#dbModalPassword').val(resp.data.password || '');
                        $('#dbModalStatus').text('点击"测试连接"验证当前配置');
                    } else {
                        $('#dbModalStatus').text('配置加载失败，请手动输入');
                    }
                } catch (e) {
                    console.error('加载数据库配置失败');
                    $('#dbModalStatus').text('配置加载失败，请手动输入');
                }
            },
            error: function() {
                $('#dbModalStatus').text('配置加载失败，请手动输入');
            }
        });
    },

    hideDbModal: function() {
        document.getElementById('dbModal').style.display = 'none';
        document.body.style.overflow = '';
    },

    showPortModal: function() {
        $.ajax({
            url: '/get-port',
            type: 'GET',
            success: function(response) {
                try {
                    var resp = typeof response === 'string' ? JSON.parse(response) : response;
                    if (resp.success) {
                        $('#portModalCurrent').val(resp.data.runningPort);
                        $('#portModalNew').val(resp.data.port);
                    }
                } catch (e) {
                    console.error('加载端口配置失败');
                }
            }
        });
        document.getElementById('portModal').style.display = 'flex';
        document.body.style.overflow = 'hidden';
    },

    hidePortModal: function() {
        document.getElementById('portModal').style.display = 'none';
        document.body.style.overflow = '';
    },

    toggleDbPassword: function() {
        var passwordInput = document.getElementById('dbModalPassword');
        var toggleIcon = document.getElementById('dbToggleIcon');
        if (passwordInput.type === 'password') {
            passwordInput.type = 'text';
            toggleIcon.classList.remove('fa-eye');
            toggleIcon.classList.add('fa-eye-slash');
        } else {
            passwordInput.type = 'password';
            toggleIcon.classList.remove('fa-eye-slash');
            toggleIcon.classList.add('fa-eye');
        }
    },

    testDbModalConnection: function() {
        var server = $('#dbModalServer').val();
        var database = $('#dbModalName').val();
        var username = $('#dbModalUser').val();
        var password = $('#dbModalPassword').val();

        console.log('测试连接 - 服务器:', server);
        console.log('测试连接 - 数据库:', database);
        console.log('测试连接 - 用户名:', username);
        console.log('测试连接 - 密码:', password ? '已输入' : '空');

        if (!server || server.trim() === '') {
            $('#dbModalResult').html('<div class="alert alert-danger"><i class="fa-solid fa-times-circle me-2"></i>请填写服务器地址</div>');
            return;
        }
        if (!database || database.trim() === '') {
            $('#dbModalResult').html('<div class="alert alert-danger"><i class="fa-solid fa-times-circle me-2"></i>请填写数据库名称</div>');
            return;
        }
        if (!username || username.trim() === '') {
            $('#dbModalResult').html('<div class="alert alert-danger"><i class="fa-solid fa-times-circle me-2"></i>请填写用户名</div>');
            return;
        }

        $.ajax({
            url: '/test-db-connection',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({server: server, database: database, username: username, password: password}),
            beforeSend: function() {
                $('#dbModalResult').html('<div class="alert alert-info"><i class="fa-solid fa-spinner fa-spin me-2"></i>正在测试连接...</div>');
            },
            success: function(response) {
                try {
                    var resp = typeof response === 'string' ? JSON.parse(response) : response;
                    if (resp.success) {
                        $('#dbModalResult').html('<div class="alert alert-success"><i class="fa-solid fa-check-circle me-2"></i>连接成功！</div>');
                    } else {
                        $('#dbModalResult').html('<div class="alert alert-danger"><i class="fa-solid fa-times-circle me-2"></i>连接失败: ' + resp.error + '</div>');
                    }
                } catch (e) {
                    $('#dbModalResult').html('<div class="alert alert-danger"><i class="fa-solid fa-times-circle me-2"></i>连接失败</div>');
                }
            },
            error: function() {
                $('#dbModalResult').html('<div class="alert alert-danger"><i class="fa-solid fa-times-circle me-2"></i>无法连接到服务器</div>');
            }
        });
    },

    saveDbModalConfig: function() {
        var server = $('#dbModalServer').val();
        var database = $('#dbModalName').val();
        var username = $('#dbModalUser').val();
        var password = $('#dbModalPassword').val();

        if (!server || !database || !username) {
            $('#dbModalResult').html('<div class="alert alert-danger"><i class="fa-solid fa-times-circle me-2"></i>请填写完整的数据库连接信息</div>');
            return;
        }

        $.ajax({
            url: '/test-db-connection',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({server: server, database: database, username: username, password: password}),
            success: function(testResp) {
                try {
                    var testResult = typeof testResp === 'string' ? JSON.parse(testResp) : testResp;
                    if (testResult.success) {
                        $.ajax({
                            url: '/update-db-config',
                            type: 'POST',
                            contentType: 'application/json',
                            data: JSON.stringify({server: server, database: database, username: username, password: password}),
                            success: function(saveResp) {
                                try {
                                    var saveResult = typeof saveResp === 'string' ? JSON.parse(saveResp) : saveResp;
                                    if (saveResult.success) {
                                        $('#dbModalResult').html('<div class="alert alert-success"><i class="fa-solid fa-check-circle me-2"></i>配置已保存并应用</div>');
                                        ServerConfig.refreshDbStatus();
                                        setTimeout(function() {
                                            ServerConfig.hideDbModal();
                                        }, 1500);
                                    } else {
                                        $('#dbModalResult').html('<div class="alert alert-danger"><i class="fa-solid fa-times-circle me-2"></i>保存失败: ' + saveResult.error + '</div>');
                                    }
                                } catch (e) {
                                    $('#dbModalResult').html('<div class="alert alert-danger"><i class="fa-solid fa-times-circle me-2"></i>保存失败</div>');
                                }
                            },
                            error: function() {
                                $('#dbModalResult').html('<div class="alert alert-danger"><i class="fa-solid fa-times-circle me-2"></i>保存失败</div>');
                            }
                        });
                    } else {
                        $('#dbModalResult').html('<div class="alert alert-danger"><i class="fa-solid fa-times-circle me-2"></i>数据库连接失败，请检查配置</div>');
                    }
                } catch (e) {
                    $('#dbModalResult').html('<div class="alert alert-danger"><i class="fa-solid fa-times-circle me-2"></i>验证失败</div>');
                }
            },
            error: function() {
                $('#dbModalResult').html('<div class="alert alert-danger"><i class="fa-solid fa-times-circle me-2"></i>无法连接到服务器</div>');
            }
        });
    },

    savePortModalConfig: function() {
        var port = $('#portModalNew').val();
        if (!port || port < 1 || port > 65535) {
            alert('请输入有效的端口号（1-65535）');
            return;
        }

        $.ajax({
            url: '/update-port',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({port: port}),
            success: function(response) {
                try {
                    var resp = typeof response === 'string' ? JSON.parse(response) : response;
                    if (resp.success) {
                        alert('端口配置已保存，重启程序后生效');
                        ServerConfig.hidePortModal();
                        ServerConfig.loadCurrentPort();
                    } else {
                        alert('保存失败: ' + resp.error);
                    }
                } catch (e) {
                    alert('保存失败');
                }
            },
            error: function() {
                alert('保存失败');
            }
        });
    },

    savePortAndRestart: function() {
        var port = $('#portModalNew').val();
        if (!port || port < 1 || port > 65535) {
            alert('请输入有效的端口号（1-65535）');
            return;
        }

        if (!confirm('确定要保存端口配置并重启服务吗？重启后需要在新端口访问。')) {
            return;
        }

        $.ajax({
            url: '/update-port',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({port: port}),
            success: function(response) {
                try {
                    var resp = typeof response === 'string' ? JSON.parse(response) : response;
                    if (resp.success) {
                        $.ajax({
                            url: '/shutdown',
                            type: 'POST',
                            success: function() {
                                alert('服务正在关闭，请手动重启程序');
                            }
                        });
                    } else {
                        alert('保存失败: ' + resp.error);
                    }
                } catch (e) {
                    alert('保存失败');
                }
            },
            error: function() {
                alert('保存失败');
            }
        });
    },

    refreshDbStatus: function() {
        $.ajax({
            url: '/get-hospital-info',
            type: 'GET',
            success: function(response) {
                try {
                    var resp = typeof response === 'string' ? JSON.parse(response) : response;
                    if (resp.success) {
                        $('#dbModalStatus').text('已连接: ' + resp.data.HospitalName);
                    } else {
                        $('#dbModalStatus').text('连接失败: ' + resp.error);
                    }
                } catch (e) {
                    $('#dbModalStatus').text('无法获取状态');
                }
            },
            error: function() {
                $('#dbModalStatus').text('无法获取状态');
            }
        });
    }
};

$(document).ready(function() {
    ServerConfig.init();
});