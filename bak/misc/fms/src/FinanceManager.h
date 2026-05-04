#ifndef FINANCEMANAGER_H
#define FINANCEMANAGER_H

#include "Transaction.h"
#include <vector>
#include <string>

class FinanceManager {
private:
    std::vector<Transaction> transactions;
    int nextId;

    // 加载交易记录
    void loadTransactions();

    // 保存交易记录
    void saveTransactions();

public:
    // 构造函数
    FinanceManager();

    // 添加交易记录
    void addTransaction(const Transaction& transaction);

    // 更新交易记录
    void updateTransaction(int id, const Transaction& transaction);

    // 删除交易记录
    void deleteTransaction(int id);

    // 获取所有交易记录
    std::vector<Transaction> getAllTransactions() const;

    // 根据日期范围获取交易记录
    std::vector<Transaction> getTransactionsByDateRange(const Date& startDate, const Date& endDate) const;

    // 根据类别获取交易记录
    std::vector<Transaction> getTransactionsByCategory(const std::string& category) const;

    // 根据类型获取交易记录
    std::vector<Transaction> getTransactionsByType(const std::string& type) const;

    // 计算总收入
    double calculateTotalIncome() const;

    // 计算总支出
    double calculateTotalExpense() const;

    // 计算净收入
    double calculateNetIncome() const;

    // 生成月度报告
    std::string generateMonthlyReport(int year, int month) const;

    // 生成年度报告
    std::string generateYearlyReport(int year) const;

    // 获取类别统计
    std::vector<std::pair<std::string, double>> getCategoryStatistics() const;
};

#endif // FINANCEMANAGER_H
