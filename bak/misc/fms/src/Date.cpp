#include "Date.h"
#include <sstream>
#include <stdexcept>

// 构造函数
Date::Date() {
    // 默认构造为当前日期
    time_t now = time(nullptr);
    struct tm* localTime = localtime(&now);
    year = localTime->tm_year + 1900;
    month = localTime->tm_mon + 1;
    day = localTime->tm_mday;
}

Date::Date(int y, int m, int d) : year(y), month(m), day(d) {
    if (!isValid()) {
        throw std::invalid_argument("Invalid date");
    }
}

Date::Date(const std::string& dateStr) {
    std::istringstream ss(dateStr);
    char delimiter;
    ss >> year >> delimiter >> month >> delimiter >> day;
    if (!isValid()) {
        throw std::invalid_argument("Invalid date format");
    }
}

// 访问器
int Date::getYear() const {
    return year;
}

int Date::getMonth() const {
    return month;
}

int Date::getDay() const {
    return day;
}

// 验证日期是否有效
bool Date::isValid() const {
    // 检查年、月、日的范围
    if (year < 1 || month < 1 || month > 12 || day < 1) {
        return false;
    }

    // 检查每月的天数
    int daysInMonth[] = {0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
    
    // 处理闰年
    if (month == 2 && ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0))) {
        return day <= 29;
    }
    
    return day <= daysInMonth[month];
}

// 转换为字符串
std::string Date::toString() const {
    std::ostringstream ss;
    ss << year << '-' << (month < 10 ? "0" : "") << month << '-' << (day < 10 ? "0" : "") << day;
    return ss.str();
}

// 运算符重载
bool Date::operator==(const Date& other) const {
    return year == other.year && month == other.month && day == other.day;
}

bool Date::operator!=(const Date& other) const {
    return !(*this == other);
}

bool Date::operator<(const Date& other) const {
    if (year != other.year) {
        return year < other.year;
    }
    if (month != other.month) {
        return month < other.month;
    }
    return day < other.day;
}

bool Date::operator<=(const Date& other) const {
    return *this < other || *this == other;
}

bool Date::operator>(const Date& other) const {
    return !(*this <= other);
}

bool Date::operator>=(const Date& other) const {
    return !(*this < other);
}

// 输入输出运算符
std::ostream& operator<<(std::ostream& os, const Date& date) {
    os << date.toString();
    return os;
}

std::istream& operator>>(std::istream& is, Date& date) {
    std::string dateStr;
    is >> dateStr;
    date = Date(dateStr);
    return is;
}
