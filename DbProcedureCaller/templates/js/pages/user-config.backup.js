var UserConfig = {
    init: function() {
        this.loadUsers();
    },

    loadUsers: function() {
        $.ajax({
            url: '/get-users',
            type: 'GET',
            success: function(response) {
                try {
                    var resp = typeof response === 'string' ? JSON.parse(response) : response;
                    if (resp.success) {
                        UserConfig.renderUsers(resp.data);
                        $('#userCount').text('共 ' + resp.data.length + ' 个用户');
                    } else {
                        UserConfig.showError('加载用户列表失败');
                    }
                } catch (e) {
                    UserConfig.showError('解析用户数据失败');
                }
            },
            error: function() {
                UserConfig.showError('无法连接到服务器');
            }
        });
    },

    renderUsers: function(users) {
        var tbody = $('#userTableBody');
        tbody.empty();
        
        if (users.length === 0) {
            tbody.html('<tr><td colspan="5" class="text-center py-10 text-gray-400">暂无用户数据</td></tr>');
            return;
        }

        users.forEach(function(user) {
            var id = user.id || user.Id || '-';
            var username = user.username || user.Username || '未设置';
            var role = user.role || user.Role || 'user';
            var status = user.status || user.Status || 'inactive';
            
            var statusClass = status === '启用' || status === 'active' ? 'uc-status-active' : 'uc-status-inactive';
            var statusText = status === '启用' || status === 'active' ? '启用' : '禁用';
            var roleText = role === '管理员' || role === 'admin' ? '管理员' : '普通用户';
            var roleClass = role === '管理员' || role === 'admin' ? 'uc-role-admin' : 'uc-role-user';
            
            var row = $('<tr>');
            row.append('<td class="uc-user-cell"><div class="uc-user-avatar">' + username.charAt(0).toUpperCase() + '</div><div><div class="uc-user-name">' + username + '</div><div class="uc-user-id">ID: ' + id + '</div></div></td>');
            row.append('<td><span class="uc-role-badge ' + roleClass + '">' + roleText + '</span></td>');
            row.append('<td><span class="uc-status-badge ' + statusClass + '">' + statusText + '</span></td>');
            row.append('<td>');
            row.append('<div style="display: flex; justify-content: center; gap: 8px;">');
            row.append('<button class="uc-action-btn uc-btn-edit" onclick="UserConfig.showEditModal(\'' + id + '\')" title="修改">&#9998;</button>');
            row.append('<button class="uc-action-btn uc-btn-delete" onclick="UserConfig.deleteUser(\'' + id + '\')" title="删除">&#128465;</button>');
            row.append('</div>');
            row.append('</td>');
            
            tbody.append(row);
        });
    },

    showAddModal: function() {
        $('#userId').val('');
        $('#userUsername').val('');
        $('#userPassword').val('');
        $('#userRole').val('user');
        $('#userStatus').val('active');
        $('#userModalLabel').text('添加用户');
        document.getElementById('userModal').classList.add('show');
        document.body.style.overflow = 'hidden';
    },

    showEditModal: function(id) {
        $.ajax({
            url: '/get-user?id=' + id,
            type: 'GET',
            success: function(response) {
                try {
                    var resp = typeof response === 'string' ? JSON.parse(response) : response;
                    if (resp.success) {
                        $('#userId').val(resp.data.Id);
                        $('#userUsername').val(resp.data.Username);
                        $('#userPassword').val('');
                        $('#userRole').val(resp.data.Role);
                        $('#userStatus').val(resp.data.Status);
                        $('#userModalLabel').text('编辑用户');
                        document.getElementById('userModal').classList.add('show');
                        document.body.style.overflow = 'hidden';
                    } else {
                        UserConfig.showError('获取用户信息失败');
                    }
                } catch (e) {
                    UserConfig.showError('解析用户数据失败');
                }
            },
            error: function() {
                UserConfig.showError('无法连接到服务器');
            }
        });
    },

    hideModal: function() {
        document.getElementById('userModal').classList.remove('show');
        document.body.style.overflow = '';
    },

    togglePassword: function() {
        var passwordInput = document.getElementById('userPassword');
        var toggleIcon = document.getElementById('toggleIcon');
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

    saveUser: function() {
        var id = $('#userId').val();
        var username = $('#userUsername').val();
        var password = $('#userPassword').val();
        var role = $('#userRole').val();
        var status = $('#userStatus').val();

        if (!username) {
            alert('请输入用户名');
            return;
        }

        var data = {
            username: username,
            password: password,
            role: role,
            status: status
        };

        var url = id ? '/update-user?id=' + id : '/add-user';

        $.ajax({
            url: url,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            success: function(response) {
                try {
                    var resp = typeof response === 'string' ? JSON.parse(response) : response;
                    if (resp.success) {
                        UserConfig.hideModal();
                        UserConfig.loadUsers();
                        UserConfig.showSuccess(id ? '用户更新成功' : '用户添加成功');
                    } else {
                        alert('操作失败: ' + resp.error);
                    }
                } catch (e) {
                    alert('操作失败');
                }
            },
            error: function() {
                alert('操作失败');
            }
        });
    },

    deleteUser: function(id) {
        if (!confirm('确定要删除该用户吗？')) {
            return;
        }

        $.ajax({
            url: '/delete-user?id=' + id,
            type: 'POST',
            success: function(response) {
                try {
                    var resp = typeof response === 'string' ? JSON.parse(response) : response;
                    if (resp.success) {
                        UserConfig.loadUsers();
                        UserConfig.showSuccess('用户删除成功');
                    } else {
                        alert('删除失败: ' + resp.error);
                    }
                } catch (e) {
                    alert('删除失败');
                }
            },
            error: function() {
                alert('删除失败');
            }
        });
    },

    showSuccess: function(message) {
        $('#userAlert').html('<div class="alert alert-success alert-dismissible fade show"><i class="fa-solid fa-check-circle me-2"></i>' + message + '<button type="button" class="btn-close" data-bs-dismiss="alert"></button></div>');
        setTimeout(function() {
            $('#userAlert').empty();
        }, 3000);
    },

    showError: function(message) {
        $('#userAlert').html('<div class="alert alert-danger alert-dismissible fade show"><i class="fa-solid fa-times-circle me-2"></i>' + message + '<button type="button" class="btn-close" data-bs-dismiss="alert"></button></div>');
        setTimeout(function() {
            $('#userAlert').empty();
        }, 3000);
    }
};

$(document).ready(function() {
    UserConfig.init();
});