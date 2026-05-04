#ifndef TRANSACTION_H
#define TRANSACTION_H

#include "Date.h"
#include <string>

class Transaction {
private:
    int id;
    Date date;
    std::string description;
    double amount;
    std::string category;
    std::string type; // "income" 或 "expense"

public:
    // 构造函数
    Transaction();
    Transaction(int id, const Date& date, const std::string& desc, double amount, const std::string& category, const std::string& type);

    // 访问器
    int getId() const;
    Date getDate() const;
    std::string getDescription() const;
    double getAmount() const;
    std::string getCategory() const;
    std::string getType() const;

    // 修改器
    void setId(int id);
    void setDate(const Date& date);
    void setDescription(const std::string& desc);
    void setAmount(double amount);
    void setCategory(const std::string& category);
    void setType(const std::string& type);

    // 转换为字符串
    std::string toString() const;
};

#endif // TRANSACTION_H
