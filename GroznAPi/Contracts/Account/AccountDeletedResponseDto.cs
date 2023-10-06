namespace Contracts.Account;

public class AccountDeletedResponseDto : DeleteAccountRequestDto
{
    public bool Deleted { get; set; }
}