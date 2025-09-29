﻿namespace Library.Shared.DTOs;

public class LoanDto
{
    public int LoanId { get; set; }  
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    
    public int BookId { get; set; }
    public int MemberId { get; set; }
}