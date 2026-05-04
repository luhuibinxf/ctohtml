#ifndef DATE_H
#define DATE_H

#include <iostream>
#include <string>

class Date {
private:
    int year;
    int month;
    int day;

public:
    // 构造函数
    Date();
    Date(int y, int m, int d);
    Date(const std::string& dateStr); // 格式：YYYY-MM-DD

    // 访问器
    int getYear() const;
    int getMonth() const;
    int getDay() const;

    // 验证日期是否有效
    bool isValid() const;

    // 转换为字符串
    std::string toString() const;

    // 运算符重载
    bool operator==(const Date& other) const;
    bool operator!=(const Date& other) const;
    bool operator<(const Date& other) const;
    bool operator<=(const Date& other) const;
    bool operator>(const Date& other) const;
    bool operator>=(const Date& other) const;

    // 输入输出运算符
    friend std::ostream& operator<<(std::ostream& os, const Date& date);
    friend std::istream& operator>>(std::istream& is, Date& date);
};

#endif // DATE_H
