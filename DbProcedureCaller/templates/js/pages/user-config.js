var UserConfig = {
    allUsers: [],
    
    init: function() {
        this.loadUsers();
    },

    loadUsers: function() {
        $.ajax({
            url: '/get-users',
            type: 'GET',
            timeout: 10000,
            success: function(response) {
                try {
                    var resp = typeof response === 'string' ? JSON.parse(response) : response;
                    if (resp.success) {
                        UserConfig.allUsers = resp.data;
                        UserConfig.renderUsers(resp.data);
                    } else {
                        UserConfig.showError('加载用户列表失败: ' + (resp.error || '未知错误'));
                    }
                } catch (e) {
                    UserConfig.showError('解析用户数据失败: ' + e.message);
                }
            },
            error: function(xhr, status, error) {
                var errorMsg = '加载用户列表失败';
                if (status === 'timeout') {
                    errorMsg = '请求超时，请检查网络连接';
                } else if (status === 'error') {
                    errorMsg = '网络错误，无法连接到服务器';
                } else if (status === 'parsererror') {
                    errorMsg = '服务器返回的数据格式不正确';
                } else {
                    errorMsg = '加载失败: ' + error;
                }
                UserConfig.showError(errorMsg);
            }
        });
    },

    filterUsers: function() {
        var searchText = $('#userSearch').val().toLowerCase();
        var filteredUsers = this.allUsers.filter(function(user) {
            var username = (user.username || user.Username || '').toLowerCase();
            return username.includes(searchText);
        });
        this.renderUsers(filteredUsers);
    },

    renderUsers: function(users) {
        var tbody = $('#userTableBody');
        tbody.empty();
        
        if (users.length === 0) {
            tbody.html('<tr><td colspan="4" class="text-center py-10 text-gray-400">暂无用户数据</td></tr>');
            return;
        }

        var totalCount = users.length;
        var activeCount = 0;
        var adminCount = 0;

        users.forEach(function(user) {
            var id = user.id || user.Id || '-';
            var username = user.username || user.Username || '未设置';
            var role = user.role || user.Role || 'user';
            var status = user.status || user.Status || 'inactive';
            
            // 统计
            if (status === '启用' || status === 'active') {
                activeCount++;
            }
            if (role === '管理员' || role === 'admin') {
                adminCount++;
            }
            
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
            row.append('<button class="uc-action-btn uc-btn-edit" onclick="UserConfig.showEditModal(\'' + id + '\')" title="修改"><i class="fa-solid fa-pencil"></i></button>');
            row.append('<button class="uc-action-btn uc-btn-delete" onclick="UserConfig.deleteUser(\'' + id + '\')" title="删除"><i class="fa-solid fa-trash"></i></button>');
            row.append('</div>');
            row.append('</td>');
            
            tbody.append(row);
        });

        // 更新统计信息
        $('#statTotal').text(totalCount);
        $('#statActive').text(activeCount);
        $('#statAdmin').text(adminCount);
        $('#statOnline').text(activeCount); // 今日在线
        $('#footerTotal').text(totalCount);
    },

    showAddModal: function() {
        $('#userId').val('');
        $('#userIdInput').val('');
        $('#userUsername').val('');
        $('#userPassword').val('');
        $('#userRole').val('user');
        $('#userStatus').val('active');
        $('#userModalLabel').text('添加用户');
        document.getElementById('userModal').classList.add('show');
        document.body.style.overflow = 'hidden';
    },

    showEditModal: function(userId) {
        console.log('编辑用户ID:', userId);
        $.ajax({
            url: '/get-user?id=' + userId,
            type: 'GET',
            timeout: 10000,
            success: function(response) {
                try {
                    var resp = typeof response === 'string' ? JSON.parse(response) : response;
                    if (resp.success) {
                        var data = resp.data;
                        $('#userId').val(data.id || data.Id || '');
                        $('#userIdInput').val(data.id || data.Id || '');
                        $('#userUsername').val(data.username || data.Username || '');
                        $('#userPassword').val('');
                        $('#userRole').val(data.role === '管理员' || data.role === 'admin' ? 'admin' : 'user');
                        $('#userStatus').val(data.status === '启用' || data.status === 'active' ? 'active' : 'inactive');
                        $('#userModalLabel').text('编辑用户');
                        document.getElementById('userModal').classList.add('show');
                        document.body.style.overflow = 'hidden';
                    } else {
                        UserConfig.showError('获取用户信息失败: ' + (resp.error || '用户不存在'));
                    }
                } catch (e) {
                    UserConfig.showError('解析用户数据失败: ' + e.message);
                }
            },
            error: function(xhr, status, error) {
                var errorMsg = '获取用户信息失败';
                if (status === 'timeout') {
                    errorMsg = '请求超时，请检查网络连接';
                } else if (status === 'error') {
                    errorMsg = '网络错误，无法连接到服务器';
                } else if (status === 'parsererror') {
                    errorMsg = '服务器返回的数据格式不正确';
                } else {
                    errorMsg = '获取用户信息失败: ' + error;
                }
                UserConfig.showError(errorMsg);
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
        var userIdInput = $('#userIdInput').val();
        var username = $('#userUsername').val();
        var password = $('#userPassword').val();
        var role = $('#userRole').val();
        var status = $('#userStatus').val();

        if (!username) {
            alert('请输入用户名');
            return;
        }

        var data = {
            id: userIdInput,
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