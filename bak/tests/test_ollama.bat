@echo off
echo ==============================================
echo  Ollama 本地服务测试脚本
echo ==============================================
echo.

echo 正在测试 Ollama 服务连接...
curl http://localhost:11434/api/tags

echo.
echo ==============================================
echo 测试完成！
echo 如果看到模型列表，说明服务正常运行。
echo 如果出现错误，请确保：
echo  1. Ollama 服务已启动 (ollama serve)
echo  2. 已拉取至少一个模型 (ollama pull llama3)
echo ==============================================
pause