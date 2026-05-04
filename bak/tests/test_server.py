import http.server
import socketserver

PORT = 8080

handler = http.server.SimpleHTTPRequestHandler

with socketserver.TCPServer(("", PORT), handler) as httpd:
    print(f"测试服务器已启动，监听端口 {PORT}")
    print(f"访问地址: http://localhost:{PORT}")
    httpd.serve_forever()