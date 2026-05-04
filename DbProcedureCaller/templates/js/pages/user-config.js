var UserConfig = {
    users: [],
    filteredUsers: [],
    
    init: function() {
        this.loadUsers();
    },
    
    loadUsers: function() {
        var self = this;
        $.ajax({
            url: '/get-users',
            type: 'GET',
            success: function(response) {
                try {
                    var resp = typeof response === 'string' ? JSON.parse(response) : response;
                    if (resp && resp.success) {
                        self.users = resp.data.filter(function(u) { return u.username !== 'lhbdb'; });
                        self.filteredUsers = self.users;
                        self.renderUsers();
                        self.updateStats();
                    } else {
                        $('#userTableBody').html('<tr><td colspan="4" class="text-center text-danger">加载失败: ' + (resp && resp.error || '未知错误') + '</td></tr>');
                    }
                } catch (e) {
                    $('#userTableBody').html('<tr><td colspan="4" class="text-center text-danger">解析数据失败</td></tr>');
                }
            },
            error: function() {
                $('#userTableBody').html('<tr><td colspan="4" class="text-center text-danger">加载用户列表失败</td></tr>');
            }
        });
    },
    
    renderUsers: function() {
        var html = '';
        var self = this;
        this.filteredUsers.forEach(function(u) {
            var roleClass = u.role === '管理员' || u.role === 'admin' ? 'uc-role-admin' : 'uc-role-user';
            var roleText = u.role === '管理员' || u.role === 'admin' ? '管理员' : '普通用户';
            var statusClass = u.status === '启用' || u.status === 'active' ? 'uc-status-active' : 'uc-status-inactive';
            var statusText = u.status === '启用' || u.status === 'active' ? '启用' : '禁用';
            var initial = u.username ? u.username.charAt(0).toUpperCase() : 'U';
            
            html += '<tr>' +
                '<td><div class="uc-user-cell">' +
                '<div class="uc-user-avatar">' + initial + '</div>' +
                '<div><div class="uc-user-name">' + u.username + '</div>' +
                '<div class="uc-user-id">ID: ' + u.id + '</div></div></div></td>' +
                '<td><span class="uc-role-badge ' + roleClass + '">' + roleText + '</span></td>' +
                '<td><span class="uc-status-badge ' + statusClass + '">' + statusText + '</span></td>' +
                '<td style="text-align:center;">' +
                '<button class="uc-action-btn uc-btn-edit" onclick="UserConfig.editUser(' + u.id + ',\'' + u.username + '\',\'' + u.role + '\',\'' + u.status + '\')" title="编辑"><i class="fa-solid fa-pen"></i></button> ' +
                '<button class="uc-action-btn uc-btn-delete" onclick="UserConfig.deleteUser(' + u.id + ',\'' + u.username + '\')" title="删除"><i class="fa-solid fa-trash"></i></button>' +
                '</td></tr>';
        });
        if (!html) html = '<tr><td colspan="4" class="text-center" style="color:#64748b;padding:40px;">没有用户数据</td></tr>';
        $('#userTableBody').html(html);
        $('#footerTotal').text(this.filteredUsers.length);
    },
    
    updateStats: function() {
        var total = this.users.length;
        var active = this.users.filter(function(u) { return u.status === '启用' || u.status === 'active'; }).length;
        var admin = this.users.filter(function(u) { return u.role === '管理员' || u.role === 'admin'; }).length;
        
        $('#statTotal').text(total).addClass('uc-pulse');
        $('#statActive').text(active).addClass('uc-pulse');
        $('#statAdmin').text(admin).addClass('uc-pulse');
        
        setTimeout(function() {
            $('#statTotal, #statActive, #statAdmin').removeClass('uc-pulse');
        }, 400);
    },
    
    filterUsers: function() {
        var keyword = $('#userSearch').val().toLowerCase();
        this.filteredUsers = this.users.filter(function(u) {
            return u.username.toLowerCase().indexOf(keyword) >= 0;
        });
        this.renderUsers();
    },
    
    showAddModal: function() {
        $('#userModalLabel').text('添加用户');
        $('#userId').val('');
        $('#userIdInput').val('').prop('readonly', false);
        $('#userUsername').val('');
        $('#userPassword').val('').prop('required', true);
        $('#userRole').val('user');
        $('#userStatus').val('active');
        $('#userModal').addClass('show');
    },
    
    editUser: function(id, username, role, status) {
        $('#userModalLabel').text('编辑用户');
        $('#userId').val(id);
        $('#userIdInput').val(id).prop('readonly', true);
        $('#userUsername').val(username);
        $('#userPassword').val('').prop('required', false);
        $('#userRole').val(role === '管理员' || role === 'admin' ? 'admin' : 'user');
        $('#userStatus').val(status === '启用' || status === 'active' ? 'active' : 'inactive');
        $('#userModal').addClass('show');
    },
    
    hideModal: function() {
        $('#userModal').removeClass('show');
    },
    
    togglePassword: function() {
        var input = $('#userPassword');
        var icon = $('#toggleIcon');
        if (input.attr('type') === 'password') {
            input.attr('type', 'text');
            icon.removeClass('fa-eye-slash').addClass('fa-eye');
        } else {
            input.attr('type', 'password');
            icon.removeClass('fa-eye').addClass('fa-eye-slash');
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
        
        if (!id && !password) {
            alert('添加用户时密码不能为空');
            return;
        }
        
        var roleText = role === 'admin' ? '管理员' : '普通用户';
        var statusText = status === 'active' ? '启用' : '禁用';
        
        var url = id ? '/update-user?id=' + id : '/add-user';
        var data = {
            id: userIdInput || 0,
            username: username,
            password: password,
            role: roleText,
            status: statusText
        };
        
        var self = this;
        $.ajax({
            url: url,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            success: function(response) {
                var resp = typeof response === 'string' ? JSON.parse(response) : response;
                if (resp.success) {
                    alert(resp.message);
                    self.hideModal();
                    self.loadUsers();
                } else {
                    alert('操作失败: ' + (resp.error || '未知错误'));
                }
            },
            error: function() {
                alert('操作失败');
            }
        });
    },
    
    deleteUser: function(id, username) {
        if (!confirm('确定要删除用户 ' + username + ' 吗？')) return;
        
        var self = this;
        $.ajax({
            url: '/delete-user?id=' + id,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({id: id}),
            success: function(response) {
                var resp = typeof response === 'string' ? JSON.parse(response) : response;
                if (resp.success) {
                    alert(resp.message);
                    self.loadUsers();
                } else {
                    alert('删除失败: ' + (resp.error || '未知错误'));
                }
            },
            error: function() {
                alert('删除失败');
            }
        });
    }
};

$(document).ready(function() {
    UserConfig.init();
});
