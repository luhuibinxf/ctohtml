#include "Transaction.h"
#include <sstream>

// 构造函数
Transaction::Transaction() : id(0), amount(0) {}

Transaction::Transaction(int id, const Date& date, const std::string& desc, double amount, const std::string& category, const std::string& type)
    : id(id), date(date), description(desc), amount(amount), category(category), type(type) {}

// 访问器
int Transaction::getId() const {
    return id;
}

Date Transaction::getDate() const {
    return date;
}

std::string Transaction::getDescription() const {
    return description;
}

double Transaction::getAmount() const {
    return amount;
}

std::string Transaction::getCategory() const {
    return category;
}

std::string Transaction::getType() const {
    return type;
}

// 修改器
void Transaction::setId(int id) {
    this->id = id;
}

void Transaction::setDate(const Date& date) {
    this->date = date;
}

void Transaction::setDescription(const std::string& desc) {
    this->description = desc;
}

void Transaction::setAmount(double amount) {
    this->amount = amount;
}

void Transaction::setCategory(const std::string& category) {
    this->category = category;
}

void Transaction::setType(const std::string& type) {
    this->type = type;
}

// 转换为字符串
std::string Transaction::toString() const {
    std::ostringstream ss;
    ss << "ID: " << id << ", Date: " << date << ", Description: " << description
       << ", Amount: " << amount << ", Category: " << category << ", Type: " << type;
    return ss.str();
}
